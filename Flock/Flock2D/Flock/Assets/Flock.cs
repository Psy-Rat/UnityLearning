using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main flocking controller
public class Flock : MonoBehaviour
{
    //prefab that we created in scene
    public FlockAgent agentPrefab;
    //List of agents that we populate with instances of prefab
    List<FlockAgent> agents = new List<FlockAgent>();
    //Current behavior
    public FlockBehavior behavior;

    //UI for hyperparameter
    [Range(10, 500)]
    public int startingCount = 250;

    //magical constant that defines
    //amount of agents that being populated
    [Range(0.005f, 1f)]
    public float AgentDensity = 0.08f;

    //multiplication factor between behaviors
    //for agents to move a little bit faster
    [Range(1f, 100f)]
    public float driveFactor = 1f;

    //Clipping result of that multiplication
    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    //Radius of neighbor agent detection
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    //Radius of avoidance behavior
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    // memoization
    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius
    {
        get
        {
            return squareAvoidanceRadius;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        
        // Start spawning
        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent" + i.ToString();
            agents.Add(newAgent);
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            //context of a neighbor radius
            List<Transform> context = GetNerbyObjects(agent);

            //Debug (colors sprite by amount of neighbors)
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);            
            
            ///*
            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            //Debug.Log(move)
            agent.Move(move);
            //*/

        }
    }

    //Getting scanning neighbor radius
    List<Transform> GetNerbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        
        //Checks all colliders in the radius
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);

        //Filtration TODO (this could be done better...)
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }

}

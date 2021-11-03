using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FlockBehavior
{
    /**
    * no neigbors -> no avoidance ajustment (resultMove = [0,0,0])
    * [othervise] -> resultMove == avg(delta(agent current position, neighbors positions))
    *
    * THIS THING HAS ADDITIONAL FILTERING
    */



    private List<Transform> filterByAvoidRadius(List<Transform> context, Vector3 agentPos, float SqAvoidRad)
    {
        return context.Where(x =>
            (Vector2.SqrMagnitude(x.position - agentPos) < SqAvoidRad)
        ).ToList();
    }

    public override Vector2 CalculateMove(
            FlockAgent agent,
            List<Transform> context,
            Flock flock)
    {
        Vector2 avoidanceMove = Vector2.zero;
        List<Transform> filteredContext = filterByAvoidRadius(context, agent.transform.position, flock.SquareAvoidanceRadius); 
        if (filteredContext.Count == 0)
            return avoidanceMove;

        foreach (Transform neighbor_data in filteredContext)
        { 
                avoidanceMove += (Vector2)(agent.transform.position - neighbor_data.position);
        }

        // avg(neighbors positions) biased agent current position
        avoidanceMove = avoidanceMove / filteredContext.Count;

        return avoidanceMove;

    }
}

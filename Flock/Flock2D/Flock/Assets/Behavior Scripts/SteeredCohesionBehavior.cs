using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FlockBehavior
{

    Vector2 currentVelocity;
    // Speed of smoothing adjustments
    public float agentSmoothTime = 0.5f;
    /**
    * no neigbors -> no cohesion ajustment (resultMove = [0,0,0])
    * [othervise] -> resultMove == avg(neighbors positions) biased agent current position
    */
    public override Vector2 CalculateMove(
            FlockAgent agent,
            List<Transform> context,
            Flock flock)
    {
        Vector2 cohesionMove = Vector2.zero;

        if (context.Count == 0)
            return cohesionMove;

        foreach (Transform neighbor_data in context)
        {
            cohesionMove += (Vector2)neighbor_data.position;
        }

        // avg(neighbors positions) biased agent current position
        cohesionMove = cohesionMove / context.Count - (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(
                agent.transform.up, 
                cohesionMove, 
                ref currentVelocity,
                agentSmoothTime);
        return cohesionMove;

    }
}

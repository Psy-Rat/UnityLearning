using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FlockBehavior
{
    /**
    * no neigbors -> no change in alignment (agent.transform.up)
    * [othervise] -> resultMove == avg(neighbors alignment)
    */
    public override Vector2 CalculateMove(
            FlockAgent agent,
            List<Transform> context,
            Flock flock)
    {
        
        Vector2 alignmentMove = agent.transform.up;

        if (context.Count == 0)
            return alignmentMove;

        foreach (Transform neighbor_data in context)
        {
            alignmentMove += (Vector2)neighbor_data.transform.up;
        }

       
        alignmentMove = alignmentMove / context.Count;

        return alignmentMove;

    }
}

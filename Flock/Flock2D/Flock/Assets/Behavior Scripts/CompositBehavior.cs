using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composit")]

public class CompositBehavior : FlockBehavior
{

    public FlockBehavior[] behaviors;
    public float[] weights;


    public override Vector2 CalculateMove(
            FlockAgent agent,
            List<Transform> context,
            Flock flock)
    {
        Vector2 resultMove = Vector2.zero;
        if (behaviors.Length != weights.Length)
        {
            Debug.LogError("Weights and Behaviors mismatch");
            return resultMove;
        }



        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock);
            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                resultMove += partialMove;
            }
        }

        return resultMove;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Restricted Volume")]
public class CenteredBehavior : FlockBehavior
{

    Vector2 center;
    public float radius = 15f;
    /**
    * no neigbors -> no cohesion ajustment (resultMove = [0,0,0])
    * [othervise] -> resultMove == avg(neighbors positions) biased agent current position
    */
    public override Vector2 CalculateMove(
            FlockAgent agent,
            List<Transform> context,
            Flock flock)
    {
        Vector2 offset = center - (Vector2)agent.transform.position;
        float t = offset.magnitude / radius;
        if (t < 0.9f)
        {
            return Vector2.zero;
        }

        return offset * t * t;


    }
}

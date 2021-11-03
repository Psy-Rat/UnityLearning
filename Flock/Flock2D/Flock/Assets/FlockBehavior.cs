using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Avoidance, Cohision, Calculation
public abstract class FlockBehavior : ScriptableObject
{
    /* CalculateMove
     * agent - current flocking agent,
     * context - have info about other agents and obstacles
     * flock - information about current type of flock behavoir itself
     */
    public abstract Vector2 CalculateMove(
            FlockAgent agent,
            List<Transform> context,
            Flock flock);
}

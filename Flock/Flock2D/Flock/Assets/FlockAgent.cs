using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We need to check obsticles and other agents
[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    Collider2D agentCollider;
    
    // accessor
    public Collider2D AgentCollider { 
        get
        {
            return agentCollider;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //initializing collider from prefab
        //(it has to be setted up there)
        agentCollider = GetComponent<Collider2D>();
    }

    //Update happens in FlockBehavior childs

    public void Move(Vector2 velocity)
    {
        //transform.forward in 3d
        transform.up = velocity;
        transform.position += (Vector3) velocity * Time.deltaTime;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereRabbitAI : MonoBehaviour
{
    public List<Vector2> patrolRoute;
    private int currentRouteId = 0;

    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void Patrol()
    {
        
    }
}

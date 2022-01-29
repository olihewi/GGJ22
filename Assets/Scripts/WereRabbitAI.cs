using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WereRabbitAI : MonoBehaviour
{
    public List<Vector2> patrolRoute = new List<Vector2>();
    public float waitTimePerPatrolStage = 1.0F;
    public float moveSpeed = 1.0F;
    public float chaseSpeedMultiplier = 2.0F;
    public float viewDistance = 3.0F;
    public float viewAngle = 235.0F;
    public LayerMask viewMask;
    private int currentRouteId = 1;
    private Vector3 lookDirection;

    private bool moving = true;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Transform target;
    [SerializeField] private Transform detectionRange;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (target == null)
        {
            if (patrolRoute.Count > 0 && moving) Patrol();
        }
        detectionRange.rotation = Quaternion.Euler(0.0F,0.0F, Vector2.SignedAngle(Vector2.up, lookDirection));
    }

    private void ResetPatrol()
    {
        target = null;
        detectionRange.localScale = Vector3.one;
        int minIndex = -1;
        float minMag = Mathf.Infinity;
        for (int i = 0; i < patrolRoute.Count; i++)
        {
            float mag = (patrolRoute[i] - new Vector2(transform.position.x, transform.position.y)).magnitude;
            if (mag < minMag)
            {
                minIndex = i;
                minMag = mag;
            }
        }
        currentRouteId = minIndex;
        StartCoroutine(TimeOut(1.5F));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 lastPos = patrolRoute[patrolRoute.Count - 1];
        foreach (Vector2 point in patrolRoute)
        {
            Gizmos.DrawLine(lastPos, point);
            lastPos = point;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + lookDirection);
    }

    private void Patrol()
    {
        rb.velocity = Vector2.MoveTowards(Vector2.zero, patrolRoute[currentRouteId] - new Vector2(transform.position.x, transform.position.y), moveSpeed * Time.deltaTime) / Time.deltaTime;
        lookDirection = rb.velocity.normalized;
        sprite.flipX = lookDirection.x > 0.0F;
        if ((new Vector2(transform.position.x, transform.position.y) - patrolRoute[currentRouteId]).magnitude < 0.1F)
        {
            currentRouteId = (currentRouteId + 1) % patrolRoute.Count;
            StartCoroutine(TimeOut(waitTimePerPatrolStage));
        }
    }

    private void Chase()
    {
        rb.velocity = Vector2.MoveTowards(Vector2.zero, target.position - transform.position, moveSpeed * chaseSpeedMultiplier * Time.deltaTime) / Time.deltaTime;
        lookDirection = rb.velocity.normalized;
        sprite.flipX = lookDirection.x > 0.0F;
    }

    private IEnumerator TimeOut(float _seconds)
    {
        moving = false;
        rb.velocity = Vector2.zero;
        float t = 0.0F;
        Vector3 targetDirection = (new Vector3(patrolRoute[currentRouteId].x, patrolRoute[currentRouteId].y, 0.0F) - transform.position).normalized;
        float rotationStep = Vector3.Angle(lookDirection, targetDirection) * Mathf.Deg2Rad / (_seconds / 2.0F);
        while (t < _seconds)
        {
            if (t / _seconds > 0.5F)
            {
                lookDirection = Vector3.RotateTowards(lookDirection, targetDirection, rotationStep * Time.deltaTime, 0.0F);
            }
            t += Time.deltaTime;
            yield return null;
        }
        moving = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!Physics2D.Raycast(transform.position, other.transform.position - transform.position, (other.transform.position - transform.position).magnitude, viewMask))
            {
                target = other.transform;
                detectionRange.localScale = Vector3.one * 1.5F;
                Chase();
            }
            else if (target != null)
            {
                ResetPatrol();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == target) ResetPatrol();
    }
    
}

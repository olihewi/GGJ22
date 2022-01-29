using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitCage : Triggerable
{
    private Vector3 startPos;
    private BoxCollider2D boxCollider;
    public bool caughtRabbit = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        startPos = transform.position;
    }

    public override void Triggered()
    {
        if (caughtRabbit) return;
        StartCoroutine(MoveTo(startPos + Vector3.back, 0.25F));
        boxCollider.enabled = true;
    }

    public override void Finished()
    {
        if (caughtRabbit) return;
        StartCoroutine(MoveTo(startPos + Vector3.forward, 0.25F));
        boxCollider.enabled = false;
    }

    private IEnumerator MoveTo(Vector3 position, float _time)
    {
        float t = 0;
        float step = (position - transform.position).magnitude / _time;
        while (t < _time)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, step * Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }
        transform.position = position;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<WereRabbitAI>() is WereRabbitAI rabbit)
        {
            rabbit.enabled = false;
            caughtRabbit = true;
        }
    }
}

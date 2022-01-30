using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitCage : Triggerable
{
    private Vector3 startPos;
    private BoxCollider2D[] boxColliders;
    public bool caughtRabbit = false;
    public GameObject door;

    private void Start()
    {
        boxColliders = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D boxCollider in boxColliders)
        {
            boxCollider.enabled = false;
        }
        startPos = transform.position;
    }

    public override void Triggered()
    {
        if (caughtRabbit) return;
        StartCoroutine(MoveTo(startPos + Vector3.back, 0.25F));
        foreach (BoxCollider2D boxCollider in boxColliders)
        {
            boxCollider.enabled = true;
        }
    }

    public override void Finished()
    {
        if (caughtRabbit) return;
        StartCoroutine(MoveTo(startPos + Vector3.forward, 0.25F));
        foreach (BoxCollider2D boxCollider in boxColliders)
        {
            boxCollider.enabled = false;
        }
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
            rabbit.caught = true;
            caughtRabbit = true;
            door.SetActive(false);
        }
    }
}

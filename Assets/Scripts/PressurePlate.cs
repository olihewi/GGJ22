using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Triggerable triggers;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger) return;
        triggers.Triggered();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.isTrigger) return;
        triggers.Held();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.isTrigger) return;
        triggers.Finished();
    }
}

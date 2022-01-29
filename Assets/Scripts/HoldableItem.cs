using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoldableItem : MonoBehaviour
{
    private bool held = false;

    private void Update()
    {
        if (!held && Input.GetButtonDown("Jump") && Vector3.Distance(transform.position, PlayerMovement.INSTANCE.transform.position) < 0.5F)
        {
            held = true;
            transform.parent = PlayerMovement.INSTANCE.GetComponentInChildren<SpriteRenderer>().transform;
            transform.localPosition = Vector3.up;
        }
        else if (held && Input.GetButtonDown("Jump"))
        {
            transform.localPosition -= Vector3.up;
            transform.parent = null;
            held = false;
        }
    }
}

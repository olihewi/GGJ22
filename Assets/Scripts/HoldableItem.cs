using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HoldableItem : MonoBehaviour
{
    private bool held = false;
    private TextMeshPro text;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshPro>();
    }

    private void Update()
    {
        bool inRange = !held && Vector3.Distance(transform.position, PlayerMovement.INSTANCE.transform.position) < 0.5F;
        text.gameObject.SetActive(inRange);
        if (inRange)
        {
            if (Input.GetButtonDown("Jump"))
            {
                held = true;
                transform.parent = PlayerMovement.INSTANCE.GetComponentInChildren<SpriteRenderer>().transform;
                transform.localPosition = Vector3.up;
            }
        }
        else if (held && Input.GetButtonDown("Jump"))
        {
            transform.localPosition -= Vector3.up;
            transform.parent = null;
            held = false;
        }
    }
}

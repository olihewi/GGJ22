using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public Camera playerCam;
    private Rigidbody2D rb;

    public static PlayerMovement INSTANCE;

    // Start is called before the first frame update
    void Start()
    {
        INSTANCE = this;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") ).normalized * moveSpeed;
        rb.velocity = dir;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SlidingBlock : MonoBehaviour
{
    bool canMove = true;
    float moveCooldown = 2.0f;
    public Vector2 moveVector = new Vector2(0, 0);

    // Abi's Spaghetti code 1/3
    [FMODUnity.EventRef]
    public string BookshelfSound;
  

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {

            moveCooldown = moveCooldown - Time.deltaTime;

            if (moveCooldown <= 0)
                canMove = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        moveCooldown = 0.2f;
        Debug.Log("Bump");
        ContactPoint2D contactPoint = collision.contacts[0];

        Vector2 moveVector = new Vector2(0,0);

        if (canMove)
        {
            
            Vector2 toOther = transform.position - collision.transform.position;

            canMove = false;

            float angle = Vector2.SignedAngle(contactPoint.normal, Vector2.up);
            Debug.Log(angle);
            if(angle == 0)
            {
                moveVector = new Vector2(0,1);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, moveVector, 1);
                Debug.DrawRay(transform.position, moveVector * 5);

                if (hit.collider)
                {
                    Debug.Log("In the way");
                }
                else
                {
                    Debug.Log("Moved");
                    this.transform.Translate(moveVector);
                    FMODUnity.RuntimeManager.PlayOneShot(BookshelfSound);
                }
            }
            
            else if (angle == 180)
            {
                moveVector = new Vector2(0, -1);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, moveVector, 1);
                Debug.DrawRay(transform.position, moveVector * 5);

                if (hit.collider)
                {
                    Debug.Log("In the way");
                }
                else
                {
                    Debug.Log("Moved");
                    this.transform.Translate(moveVector);
                FMODUnity.RuntimeManager.PlayOneShot(BookshelfSound);
            }
            }
            else if(angle == 90)
            {
                moveVector = new Vector2(1, 0);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, moveVector, 1);
                Debug.DrawRay(transform.position, moveVector * 5);

                if (hit.collider)
                {
                    Debug.Log("In the way");
                }
                else
                {
                    Debug.Log("Moved");
                    this.transform.Translate(moveVector);
                FMODUnity.RuntimeManager.PlayOneShot(BookshelfSound);
            }
            }
            else if (angle == -90)
            {
                moveVector = new Vector2(-1,0);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, moveVector, 1);
                Debug.DrawRay(transform.position, moveVector * 5);

                if (hit.collider)
                {
                    Debug.Log("In the way");
                }
                else
                {
                    Debug.Log("Moved");
                    this.transform.Translate(moveVector);
                    FMODUnity.RuntimeManager.PlayOneShot(BookshelfSound);
                }
            }
        }
    }
}

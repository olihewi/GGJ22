using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SlidingBlock : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int tileLocation;
    bool canMove = true;
    float moveCooldown = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tilemapObj = GameObject.FindGameObjectWithTag("TileMap");
        tilemap = tilemapObj.GetComponent<Tilemap>();
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

        Vector2 moveVector;

        if (canMove)
        {
            Vector2 toOther = transform.position - collision.transform.position;

            canMove = false;
            if (Vector2.Dot(Vector2.up, toOther) < 0)
            {
                if (contactPoint.point.x > this.transform.position.x)
                {
                    moveVector = new Vector2(-1, 0);
                }
                else
                {
                    moveVector = new Vector2(1, 0);
                }

                RaycastHit2D hit = Physics2D.Raycast(transform.position, moveVector, 1);

                if (hit.collider)
                {
                    Debug.Log("In the way");
                }
                else
                {
                    Debug.Log("Moved");
                    this.transform.Translate(moveVector);
                }
            }
            else if (Vector2.Dot(Vector2.right, toOther) < 0)
            {
                if (contactPoint.point.y > this.transform.position.y)
                {
                    moveVector = new Vector2(0, -1);
                }
                else
                {
                    moveVector = new Vector2(0, 1);
                }

                RaycastHit2D hit = Physics2D.Raycast(transform.position, moveVector, 1);

                if (hit.collider)
                {
                    Debug.Log("In the way");
                }
                else
                {
                    Debug.Log("Moved");
                    this.transform.Translate(moveVector);
                }
            }




        }
    }
}

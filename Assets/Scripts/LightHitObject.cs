using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightHitObject : MonoBehaviour
{
    
    public List<GameObject> objectToTrack;
    float cornerNumber = 2.6f;
    public float minAngle;
    public float maxAngle;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir = -transform.up;
        dir = transform.InverseTransformDirection(dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        maxAngle = angle + GetComponent<Light2D>().pointLightOuterAngle;
        minAngle = angle - GetComponent<Light2D>().pointLightOuterAngle;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var obj in objectToTrack)
        {

            Vector3 dir = obj.transform.position - transform.position;
            dir = obj.transform.InverseTransformDirection(dir);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


            Vector2 topLeftDir = -(transform.position - new Vector3(obj.transform.position.x - obj.transform.localScale.x / cornerNumber, obj.transform.position.y + obj.transform.localScale.y / cornerNumber));
            Vector2 topRightDir = -(transform.position - new Vector3(obj.transform.position.x + obj.transform.localScale.x / cornerNumber, obj.transform.position.y + obj.transform.localScale.y / cornerNumber));
            Vector2 bottomLeftDir = -(transform.position - new Vector3(obj.transform.position.x - obj.transform.localScale.x / cornerNumber, obj.transform.position.y - obj.transform.localScale.y / cornerNumber));
            Vector2 bottomRightDir = -(transform.position - new Vector3(obj.transform.position.x + obj.transform.localScale.x / cornerNumber, obj.transform.position.y - obj.transform.localScale.y / cornerNumber));
            float dist = Vector2.Distance(transform.position, obj.transform.position);

            //Debug.Log(dist);
            if(dist <= GetComponent<Light2D>().pointLightOuterRadius)
            {
                RaycastHit2D hitTopLeft = Physics2D.Raycast(transform.position, topLeftDir, dist);
                RaycastHit2D hitTopRight = Physics2D.Raycast(transform.position, topRightDir, dist);
                RaycastHit2D hitBottomLeft = Physics2D.Raycast(transform.position, bottomLeftDir, dist);
                RaycastHit2D hitBottomRight = Physics2D.Raycast(transform.position, bottomRightDir, dist);


                //if((angle >= minAngle && angle <= maxAngle) && (dist <= GetComponent<Light2D>().pointLightOuterRadius))
                //{
                //    Debug.Log("help");
                //}


                if (((hitBottomLeft.collider == obj.GetComponent<Collider2D>() || hitBottomRight.collider == obj.GetComponent<Collider2D>()) || (hitTopLeft.collider == obj.GetComponent<Collider2D>()
                    || hitTopRight.collider == obj.GetComponent<Collider2D>())) && ((angle >= minAngle && angle <= maxAngle)))
                {
                    InLight(obj);
                }
                else
                {
                    InShade(obj);
                }
            }



        }
    }

    void InLight(GameObject obj)
    {
        Debug.Log(obj.name + " is being hit by light, call function to alter state");
    }

    void InShade(GameObject obj)
    {
        Debug.Log(obj.name + " is in the shade, call function to alter state");
    }

    private void OnDrawGizmos()
    {
        if(objectToTrack.Count > 0)
        {
            Vector2 topLeftDir = -(transform.position - new Vector3(objectToTrack[0].transform.position.x - objectToTrack[0].transform.localScale.x / cornerNumber, objectToTrack[0].transform.position.y + objectToTrack[0].transform.localScale.y / cornerNumber));
            Vector2 topRightDir = -(transform.position - new Vector3(objectToTrack[0].transform.position.x + objectToTrack[0].transform.localScale.x / cornerNumber, objectToTrack[0].transform.position.y + objectToTrack[0].transform.localScale.y / cornerNumber));
            Vector2 bottomLeftDir = -(transform.position - new Vector3(objectToTrack[0].transform.position.x - objectToTrack[0].transform.localScale.x / cornerNumber, objectToTrack[0].transform.position.y - objectToTrack[0].transform.localScale.y / cornerNumber));
            Vector2 bottomRightDir = -(transform.position - new Vector3(objectToTrack[0].transform.position.x + objectToTrack[0].transform.localScale.x / cornerNumber, objectToTrack[0].transform.position.y - objectToTrack[0].transform.localScale.y / cornerNumber));
            Debug.DrawRay(transform.position, topLeftDir);
            Debug.DrawRay(transform.position, topRightDir);
            Debug.DrawRay(transform.position, bottomLeftDir);
            Debug.DrawRay(transform.position, bottomRightDir);
        }

    }
}

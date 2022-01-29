using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Experimental.Rendering.Universal;

public class ObjectInLight : MonoBehaviour
{
    public bool isInLight;
    public GameObject lightSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, lightSource.transform.position) > lightSource.GetComponent<Light2D>().pointLightOuterRadius)
        {
            isInLight = false;
        }
    }
}

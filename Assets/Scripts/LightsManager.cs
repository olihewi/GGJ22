using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightsManager : MonoBehaviour
{
    public List<GameObject> lights;
    public List<GameObject> objectsToTrack;
    int numberOfLights;
    int index = 0;
    int objCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var light in lights)
        {

            foreach (var obj in objectsToTrack)
            {

                if (Vector2.Distance(obj.transform.position, light.transform.position) <= light.GetComponent<Light2D>().pointLightOuterRadius)
                {
                    if(light.GetComponent<LightHitObject>().objectToTrack.Count == 0)
                    {
                        light.GetComponent<LightHitObject>().objectToTrack.Add(obj);
                    }
                    else if(light.GetComponent<LightHitObject>().objectToTrack.Count > 0 && index < light.GetComponent<LightHitObject>().objectToTrack.Count)
                    {
                        if(light.GetComponent<LightHitObject>().objectToTrack[index] != obj)
                        {
                            light.GetComponent<LightHitObject>().objectToTrack.Add(obj);
                        }
                        
                    }
                    if(index < light.GetComponent<LightHitObject>().objectToTrack.Count)
                    {
                        index++;
                    }
                    
                    
                }
                else
                {
                    light.GetComponent<LightHitObject>().objectToTrack.Remove(obj);
                }
            }
        }
        index = 0;




    }
}

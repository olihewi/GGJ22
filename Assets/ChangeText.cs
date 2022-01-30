using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;


public class ChangeText : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Text(string text)
    {
        GetComponent<TextMeshPro>().text = text;
    }
}

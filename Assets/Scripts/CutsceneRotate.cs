using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneRotate : MonoBehaviour
{
    public Sprite left;
    public Sprite right;
    public Sprite front;

    // Start is called before the first frame update
    void Start()
    {
        front = GetComponent<SpriteRenderer>().sprite;
    }

    public void RotateLeft()
    {
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        GetComponent<SpriteRenderer>().sprite = left;
        
    }

    public void RotateRight()
    {
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        GetComponent<SpriteRenderer>().sprite = right;
        
    }

    public void RotateFront()
    {
        GetComponent<RectTransform>().localScale = new Vector3(20, 20, 1);
        GetComponent<SpriteRenderer>().sprite = front;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainsController : MonoBehaviour
{
    public GameObject   CurtainsOpen;
    public GameObject   CurtainsClosed;
    public BoxCollider  lightCollider;
    public GameObject   Player;
    public Renderer[] PlayerRenderers;
    public GameObject respawn;

    public Vector4 PlayerRGBA   = new Vector4(1,1,1,1); 
    public Vector4 BurningRGBA  = new Vector4(255 , 165 , 0 , 255);
    public Vector4 CurrentRGBA;

    public bool isOccupied;
    public bool burning;

    public int count;
    private bool flipped;
    // Start is called before the first frame update
    void OnEnable()
    {
        lightCollider = GetComponentInChildren<BoxCollider>(CurtainsOpen);
        StartCoroutine(SwapCurtains());
        PlayerRenderers = Player.GetComponentsInChildren<Renderer>();
        CurrentRGBA = PlayerRGBA;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (CurtainsOpen.activeInHierarchy && isOccupied)
        {
            burning = true;
            CurrentRGBA = Color.Lerp(CurrentRGBA, BurningRGBA, Time.deltaTime * .001f);
            
        }

        if (!burning)
        {
            count = 0;
            CurrentRGBA = /*Color.Lerp(CurrentRGBA, PlayerRGBA, Time.deltaTime * .01f)*/ Color.white;

        }

        for (int i = 0; i < PlayerRenderers.Length; i++)
        {
            PlayerRenderers[i].material.color = CurrentRGBA;
        }
        if (burning && count == 0)
        {
            count++;
            StartCoroutine(Burning());
        }
        /*for (int i = 0; i < PlayerRenderers.Length; i++)
        {
            CurrentRGBA = Color.Lerp(CurrentRGBA, PlayerRGBA, Time.deltaTime * .5f);
            PlayerRenderers[i].material.color = CurrentRGBA;
        }*/

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOccupied = true;
        if (isOccupied && burning)
        {
            StartCoroutine(Burning());
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (CurtainsOpen.active  && isOccupied)
        {
            burning = true;
            //burning = true;
            
            //CurrentRGBA = Color.Lerp(PlayerRGBA, BurningRGBA, Time.deltaTime);
            
            //Debug.Log("player spotted!!!!!!!!!! lolllll!!!");
        }
        else
        {
            burning = false;
        }
    }*/
    private void OnTriggerExit2D(Collider2D collision)
    {
        isOccupied = false;
        Debug.Log("left");
        burning = false;
        count = 0;
        StopCoroutine(Burning());
    }

    void SwitchCurtains()
    {
        if (!flipped)
        {
            CurtainsOpen.SetActive(true);
            CurtainsClosed.SetActive(false);

        }
        if (flipped)
        {
            CurtainsOpen.SetActive(false);
            CurtainsClosed.SetActive(true);
            burning = false;
        }
        flipped = !flipped;
    }

    IEnumerator SwapCurtains()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            
            //Debug.Log("Ding " + count);
            //count++;
            SwitchCurtains();
        }
    }
    IEnumerator Burning()
    {

        yield return new WaitForSeconds(2.3f);

        if (burning)
        {
            Debug.Log("Ding " + count);
            count = 0;
            Player.transform.position = respawn.transform.position;
        }
        else
        {
            yield return null;
        }
    }
}


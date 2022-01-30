using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainsController : MonoBehaviour
{
    public GameObject   CurtainsOpen;
    public GameObject   CurtainsClosed;

    public GameObject   Player;
    public Renderer[] PlayerRenderers;
    public GameObject respawn;

    [Range(0f, 5f)]     public float InitialDelay = 30f;
    [Range(1f,5f)]      public float OpenFor = 3f;
    [Range(1f, 5f)]     public float ClosedFor = 3f;
    [Range(.5f, 4f)]    public float BurnSpeed = 2.3f;

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
       
        StartCoroutine(OpenAndCloseCurtains());
        
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
    private void LateUpdate()
    {
        if (isOccupied)
        {
            for (int i = 0; i < PlayerRenderers.Length; i++)
            {
                PlayerRenderers[i].material.color = CurrentRGBA;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        initPlayer(collision);
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

        removePlayer();
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

    IEnumerator OpenAndCloseCurtains()
    {
        while (true)
        {
            if (flipped)
            {
                yield return new WaitForSeconds(OpenFor);
            }
            else
            {
                yield return new WaitForSeconds(ClosedFor);
            }
            
            
            //Debug.Log("Ding " + count);
            //count++;
            SwitchCurtains();
        }
    }
    IEnumerator Burning()
    {

        yield return new WaitForSeconds(BurnSpeed);

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
    void initPlayer(Collider2D collision)
    {
        Player = collision.gameObject;
        PlayerRenderers = Player.GetComponentsInChildren<Renderer>();
    }
    void removePlayer()
    {
        for (int i = 0; i < PlayerRenderers.Length; i++)
        {
            PlayerRenderers[i].material.color = Color.white;
            PlayerRenderers[i] = null;
        }
        Player = null;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainsController : MonoBehaviour
{
    public GameObject   CurtainsOpen;
    public GameObject   CurtainsClosed;
    public BoxCollider  lightCollider;
    public GameObject   Player;

    private int count;
    private bool flipped;
    // Start is called before the first frame update
    void OnEnable()
    {
        lightCollider = GetComponentInChildren<BoxCollider>(CurtainsOpen);
        StartCoroutine(DropAttack());
    }

    // Update is called once per frame
    void Update()
    {

            
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && CurtainsOpen.active)
        {
            Debug.Log("player spotted!!!!!!!!!! lolllll!!!");
        }
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
        }
        flipped = !flipped;
    }

    IEnumerator DropAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);

            Debug.Log("Ding " + count);
            count++;
            SwitchCurtains();
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsGrass : MonoBehaviour
{
    [FMODUnity.BankRef]
    public string inputSound;
    bool playerIsMoving;
    public float walkingSpeed;

    void Update()
    {
        if (Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
        {
            //Debug.Log ("Player is moving");
            playerIsMoving = true;
        }
        else if (Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") == 0)
        {
            //Debug.Log ("Player is not moving");
            playerIsMoving = false;
        }
    }


    void CallFootsteps()
    {
        if (playerIsMoving == true)
        {
            //Debug.Log ("Player is moving");
            FMODUnity.RuntimeManager.PlayOneShot(inputSound);
        }
    }

    void Start()
    {
        InvokeRepeating("CallFootsteps", 0, walkingSpeed);
    }


    void OnDisable()
    {
        playerIsMoving = false;
    }
}

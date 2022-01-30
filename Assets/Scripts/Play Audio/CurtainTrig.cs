//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CurtainTrig : MonoBehaviour
//{
//    [FMODUnity.BankRef]
//    public string inputSound1,inputSound2;
//    bool curtainOpen;
//    bool curtainClose;
//    public CurtainsController CurtainsController;

//    //void Update()
//    //{
//    //      curtainsClosed = curtainClose;
//    //      curtainsOpen = curtainOpen;
//    //}

//    void CurtainCall()
//    {
//        if (CurtainsController.flipped)
//        {
//            Debug.Log ("Player is moving");
//            FMODUnity.RuntimeManager.PlayOneShot(inputSound1);
//        }
//        if (!CurtainsController.flipped)
//        {
//            Debug.Log ("Player is moving");
//            FMODUnity.RuntimeManager.PlayOneShot(inputSound2);
//        }
//    }
//}

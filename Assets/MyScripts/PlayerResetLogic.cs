using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetLogic : MonoBehaviour
{
    public Vector3 startPosition;
    private GameObject playerArmature;
    private CharacterController cc;
    private float originalGravity;
    private ThirdPersonController tpc;


    private void Start()
    { 
        playerArmature = GameObject.FindGameObjectWithTag("Player");
        tpc = playerArmature.GetComponent<ThirdPersonController>();
        originalGravity = tpc.Gravity; 
        cc = playerArmature.GetComponent<CharacterController>();    
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player")) 
        {
            // disable the character controller and set the gravity to 0 in order
            // to reset the player position
            cc.enabled = false;
            tpc.Gravity = 0;
            other.gameObject.transform.position = startPosition;
            tpc.Gravity = originalGravity;
            cc.enabled = true;
        }
    }
}

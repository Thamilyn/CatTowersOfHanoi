using JetBrains.Annotations;
using Retro.ThirdPersonCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleControl : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    public float consoleDistance = 3f;
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private Camera catCamera;
    public CatController catController;
    private bool isCatCamActive = false;
    private Animator animator;

    private void Start()
    {
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < consoleDistance) 
        {

            if (Input.GetMouseButton(0))
            {
                EnableCatCamera();
            }
            else if (!isCatCamActive)
            {
                animator.SetBool("DoMoveWheels", true);
            }
        }
        else
        {
            animator.SetBool("DoMoveWheels", false);
        }


        if ( Input.GetMouseButton(1) && isCatCamActive)
        {
           EnablePlayerCamera();
        }
        
    }

    public void EnableCatCamera()
    {
        if (!isCatCamActive)
        {
            SoundManager.instance.PlaySoundEffect(4);
        }
        // change the priority of the virtual cameras, resulting in the catCamera becoming the active camera
        //playerCamera.Priority = 10;
        //virtualCamera.Priority = 15;
        
        playerCamera.enabled = false;
        catCamera.enabled = true;   
        isCatCamActive = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.GetComponent<Movement>().enabled = false;
        
        catController.EnableMove();
    }

    public void EnablePlayerCamera()
    {
        //change the priority of the virtual cameras, resulting in the playerCamera becoming the active camera
       // playerCamera.Priority = 15;
        //virtualCamera.Priority = 10;
        playerCamera.enabled = true;
        catCamera.enabled = false;
        isCatCamActive = false;
        Cursor.visible = false;
        catController.DisableMove();
        player.GetComponent<Movement>().enabled = true;
    }
}

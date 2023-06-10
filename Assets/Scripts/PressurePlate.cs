using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject[] doorsToOpen;
    public GameObject doorToClose;
    private bool isPressed = false;
    private Animation anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    private void PressButon()
    {
        anim.Play("plate_down");
        // we can have doors that open and close simultaneosly for a plate
        foreach (var d in doorsToOpen)
        {
            d.GetComponent<Reja>().OpenDoor();
        }
        if (doorToClose != null) 
        { 
            doorToClose.GetComponent<Reja>().CloseDoor();
        }
        isPressed = true;
    }

    public void UnPressButon()
    {
        anim.Play("plate_up");
        foreach (var d in doorsToOpen)
        {
            d.GetComponent<Reja>().CloseDoor();
        }
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        // if we press the pressure plate activate it
        if (other.CompareTag("Player") && !isPressed)
        {
            PressButon();   
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        // If we are standing on a plate and press fire1 deactivate it
        //if (other.CompareTag("InteractButton") && isPressed && Input.GetButton("Fire1"))
        //{
        //    UnPressButon();
        //}
    }
}

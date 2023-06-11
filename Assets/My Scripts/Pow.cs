using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pow : MonoBehaviour
{
    public GameObject door;
    public GameObject bombPosition;
    public GameObject bomb;
    private GameObject carriedOrb;
    public GameObject orbPosition;
    public GameObject intendedOrbToUse;
    public bool IsOnPillarArea = false;
    public GameObject PillarToUse;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    void Update()
    {
        //put bombs
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bomb,bombPosition.transform.position,Quaternion.identity);
        }
        //restore all the doors
        if (Input.GetKeyDown("r"))
        {
            var doors = GameObject.FindGameObjectsWithTag("Jail");
            foreach (var door in doors)
            {
                door.GetComponent<Jail>().Restore();
            }
        }
        // if we are carrying an orb and press right mouse button, drop it on the floor
        if (Input.GetMouseButtonDown(1) && carriedOrb != null)
        {
            carriedOrb.transform.parent = null;
            carriedOrb.transform.position = bombPosition.transform.position;
            ReleaseOrb();
        }

        if (Input.GetKeyDown("e"))
        {
            // if we are interacting with a pillar
            if (IsOnPillarArea && PillarToUse != null)
            {
                // if we have an orb, put it on top of the pillar
                var pillar = PillarToUse.GetComponent<Pillar>();
                if (carriedOrb != null)
                {
                    pillar.PutOrb(carriedOrb);
                    ReleaseOrb();
                }
                // otherwise we grab the orb from the pillar
                else
                {
                    GrabOrb(pillar.GetPillarOrb());
                    pillar.Release();
                }
            }
           
        }
    }



    //player carry orbs
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // if we are colliding with an orb and press the e button
        if(hit.collider.CompareTag("Orb") 
            && Input.GetKeyDown("e") 
            && carriedOrb == null
            && !IsOnPillarArea) 
        {
            GrabOrb(hit.collider.gameObject);
        }

    }


    // grab an orb and put it on the orbPosition
    public void GrabOrb(GameObject orb)
    {
        carriedOrb = orb;
        carriedOrb.transform.parent = transform;
        carriedOrb.transform.position = orbPosition.transform.position;

    }

    public bool HasOrb() { return carriedOrb != null; }

    public GameObject GetCurrentOrb() { return carriedOrb; }
    public void ReleaseOrb()
    {
        carriedOrb = null;
    }
        
}


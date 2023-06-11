using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public bool hasOrb = false;
    public Transform orbTransform;
    public GameObject correctOrb;
    private GameObject currentOrb;
    private AudioSource audioSrc;
   
    
    // Start is called before the first frame update
    void Start()
    {
        audioSrc= GetComponent<AudioSource>();
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // if the player is on area
        // if it has an orb
        if (other.CompareTag("Player"))
        {
            //bool isActionPressed = Input.GetKeyDown("e");
            var p = other.GetComponent<Pow>();
            var orb = p.GetCurrentOrb();
            p.IsOnPillarArea = true;
            p.intendedOrbToUse = orb;
            p.PillarToUse = gameObject;

           
        }
      
    }

    public GameObject GetPillarOrb() { return currentOrb; }

    //put the orb on top of the pillar
    public void PutOrb(GameObject orb)
    {
        orb.transform.parent = transform;
        orb.transform.position = orbTransform.position;
        currentOrb = orb;
        audioSrc.Play();
        hasOrb = true;
    }

    // detect when the player exits the pillar interactive zone.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var pow = other.GetComponent<Pow>();
            pow.IsOnPillarArea = false;
            pow.PillarToUse = null;
        }
    }

    public bool HasCorrectOrb()
    {
        return currentOrb == correctOrb;
    }

    //The pillar's orb reference is null
    public void Release()
    {
        currentOrb = null;
    }
}

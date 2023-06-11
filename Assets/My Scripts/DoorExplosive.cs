using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExplosive : MonoBehaviour
{
    private GameObject arc;
    private GameObject grid;
    public float explosionForce = 200f;
    public float explosionRadius = 5f;
    private Vector3 gridStart;
    private Quaternion gridRotation;
    private AudioSource audioSrc;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        arc= transform.GetChild(0).gameObject;
        grid= transform.GetChild(1).gameObject;
        gridStart = grid.transform.position;
        gridRotation = grid.transform.rotation;
        var rb = grid.AddComponent<Rigidbody>();
        rb.mass = 0.5f;
        rb.isKinematic= true;
    }
   
    //Jail destruction
    public void Explode()
    {
        //Destroy(transform.GetChild(0).gameObject);
        audioSrc.Play();
        arc.GetComponent<MeshRenderer>().enabled=false;
        GetComponent<BoxCollider>().enabled = false;
        var rb = grid.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddExplosionForce(explosionForce,transform.position,explosionRadius);
    }

    //Jail reset
    public void Restore()
    {
        arc.GetComponent<MeshRenderer>().enabled=true;
        grid.transform.position = gridStart;
        grid.transform.rotation = gridRotation;
        grid.GetComponent<Rigidbody>().isKinematic=true;
        GetComponent<BoxCollider>().enabled=true;
    }
   
}

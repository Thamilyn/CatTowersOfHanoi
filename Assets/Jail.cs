using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jail : MonoBehaviour
{
    private GameObject reja;
    private GameObject orb;
    private Vector3 orbOriginalPos;
    // Start is called before the first frame update
    void Start()
    {
        reja = transform.GetChild(0).gameObject;
        orb = transform.GetChild(1).gameObject;
        orbOriginalPos = orb.transform.position;
    }


    public void Restore()
    {
        // restore the jail by resetting the grid, the door and the orb to it's original positions
        var orbParent = orb.transform.parent;
        if (orbParent != null )
        {
            var pillar = orb.transform.parent.GetComponent<Pillar>();
            if (pillar != null) pillar.Release();
        }

        orb.transform.parent = transform;
        orb.transform.position = orbOriginalPos;
        reja.GetComponent<DoorExplosive>().Restore();   
    } 

    
}

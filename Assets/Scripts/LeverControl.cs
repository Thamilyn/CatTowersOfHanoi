using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverControl : MonoBehaviour
{
    [SerializeField]
    private PressurePlate[] pressurePlates;
    [SerializeField]
    private float raycastDistance = 2.5f;
    [SerializeField]
    private GameObject leverMain;
    private Animation leverAnimation;

    // Start is called before the first frame update
    void Start()
    {
        leverAnimation= GetComponent<Animation>();
    }

    private void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 6;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(leverMain.transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastDistance, layerMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                leverAnimation.Play("up_lever");
                ResetPlates();
            }
        }
    }

    private void ResetPlates()
    {
        for (int i = 0; i < pressurePlates.Length; i++)
        {
            var plate = pressurePlates[i];
            plate.UnPressButon();
        }
    }
}

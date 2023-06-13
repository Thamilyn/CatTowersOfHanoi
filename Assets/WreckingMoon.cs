using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingMoon : MonoBehaviour
{
    [SerializeField]
    private HingeJoint pivotJoint;

    public void ActivateMoon()
    {
        pivotJoint.axis= new Vector3(0, 0, 1);
        pivotJoint.enablePreprocessing= true;
        pivotJoint.useMotor= true;
    } 
}

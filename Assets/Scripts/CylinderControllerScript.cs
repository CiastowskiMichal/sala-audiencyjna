using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderControllerScript : MonoBehaviour
{

    [SerializeField]
    private CylinderScript cylinder;

    [SerializeField]
    private bool isUpward;

    public CylinderScript GetCylinderScript()
    {
        return cylinder.GetComponent<CylinderScript>();
    }

    public float GetAngle()
    {
        if (isUpward)
        {
            return -45f;
        }

        return 45f;
    }
}

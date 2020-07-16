using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private float openSpeed;
    private bool shouldOpen;

    void Start()
    {
        shouldOpen = false;
    }
    
    public void SetShouldOpen(bool state)
    {
        shouldOpen = state;
    }

    public bool ShouldOpen()
    {
        return shouldOpen;
    }

    public void OpenDoor()
    {
        Vector3 from = transform.localRotation.eulerAngles;
        Vector3 destination = from;
        destination.y = 90;

        Quaternion fromQuaternion = Quaternion.Euler(from);
        Quaternion destinationQuaternion = Quaternion.Euler(destination);
        transform.localRotation = Quaternion.Lerp(fromQuaternion, destinationQuaternion, Time.deltaTime * openSpeed);
    }
}

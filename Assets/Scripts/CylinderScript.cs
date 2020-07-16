using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderScript : MonoBehaviour
{
    private float correctX;
    private float destinationX;

    private Vector3 correctRotation;

    public float GetCorrectX()
    {
        return correctX;
    }
    public Vector3 GetRotationAngles()
    {
        return transform.localRotation.eulerAngles;
    }

    private void Awake()
    {
        correctRotation = transform.localRotation.eulerAngles;
        Debug.Log(name + " " + correctRotation);
        correctX = transform.localRotation.eulerAngles.x;
        destinationX = correctX;
    }

    public bool IsUnlocked()
    {
        float angle = Quaternion.Angle(Quaternion.Euler(correctRotation), transform.localRotation);
        if (angle >= -1.5f && angle <= 1.5f )
        {
            return true;
        }
        return false;
    }

    public void SetDestinationX(float newDestinationX)
    {
        destinationX = newDestinationX;
    }

    public float GetDestinationX()
    {
        return destinationX;
    }

    public void ChangeDestinationX(float newDestinationX)
    {
        destinationX += newDestinationX;
        destinationX = (destinationX + 360) % 360;
    }

    public void SetRandomPosition()
    {
        int random = UnityEngine.Random.Range(0, 8);
        Vector3 randomRotation = correctRotation;
        randomRotation.x = (20 + 45 * random) % 360;
        transform.localRotation = Quaternion.Euler(randomRotation.x, 270, 270);
        destinationX = randomRotation.x;
        Debug.Log(name + " " + transform.localRotation.eulerAngles);
    }

    public void RotateCylinder()
    {
        Vector3 from = transform.localRotation.eulerAngles;
        Vector3 destination = correctRotation;
        destination.x = (int) destinationX;
        destination.y = 270;
        destination.z = 270;

        Quaternion fromQuaternion = Quaternion.Euler(from);
        Quaternion destinationQuaternion = Quaternion.Euler(destination);
        transform.localRotation = Quaternion.Lerp(fromQuaternion, destinationQuaternion, Time.deltaTime * 3f);
    }
}

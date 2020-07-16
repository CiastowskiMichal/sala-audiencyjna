using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuteCameraChange : MonoBehaviour
{
    [SerializeField]
    Camera cam1;
    [SerializeField]
    Camera cam2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void CameraChangeCompleted()
    {
        cam1.enabled = true;
        cam2.enabled = false;
        //GetComponent<Animator>().enabled = false;
    }
    void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}

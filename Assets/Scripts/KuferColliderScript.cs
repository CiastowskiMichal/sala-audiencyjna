using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuferColliderScript : MonoBehaviour
{
    [SerializeField]
    GameObject Przykrywa;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.childCount);
        if (transform.childCount <= 0)
        {
            Przykrywa.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}

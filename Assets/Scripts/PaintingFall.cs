using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingFall : MonoBehaviour
{
    bool soundplayed;
    // Start is called before the first frame update
    void Start()
    {
        soundplayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("BookSpace")==null && !soundplayed)
        { 
            this.transform.position = new Vector3(-0.6f,0, -2.536f);
            this.transform.rotation = Quaternion.Euler(0,-38,0);
            GetComponent<AudioSource>().Play();
            soundplayed = true;
        }
    }
}

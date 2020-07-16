using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SterowanieController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && !GetComponent<Animator>().enabled)
        {
            GetComponent<Animator>().enabled = true;
        }
    }
    public void PauseCamera()
    {
        GetComponent<Animator>().enabled = false;
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
}

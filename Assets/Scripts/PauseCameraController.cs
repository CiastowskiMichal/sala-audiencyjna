using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseCameraController : MonoBehaviour
{
    [SerializeField]
    Image control1;
    [SerializeField]
    Image control2;
    [SerializeField]
    Text continueText;
    private float alphaTemp = 0;
    // Start is called before the first frame update
    void Start()
    {
        control1.color = new Vector4(control1.color.r, control1.color.g, control1.color.b, alphaTemp);
        control2.color = new Vector4(control2.color.r, control2.color.g, control2.color.b, alphaTemp);
        continueText.color = new Vector4(continueText.color.r, continueText.color.g, continueText.color.b, alphaTemp);
    }

    // Update is called once per frame
    void Update()
    {
        control1.color = new Vector4(control1.color.r, control1.color.g, control1.color.b, Mathf.Lerp(control1.color.a, alphaTemp, 0.2f));
        control2.color = new Vector4(control2.color.r, control2.color.g, control2.color.b, Mathf.Lerp(control2.color.a, alphaTemp, 0.2f));
        continueText.color = new Vector4(continueText.color.r, continueText.color.g, continueText.color.b, Mathf.Lerp(continueText.color.a, alphaTemp, 0.2f));
        //Debug.Log(GetComponent<Animator>().enabled);
        if (!GetComponent<Animator>().enabled)
        {
            alphaTemp = 1;
        }
        else
        {
            alphaTemp = 0;
        }
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
        SceneManager.LoadScene("Sterowanie", LoadSceneMode.Single);
    }
}

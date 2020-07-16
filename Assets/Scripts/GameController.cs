using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject camera1;
    [SerializeField]
    GameObject camera2;
    [SerializeField]
    GameObject rotator;
    [SerializeField]
    GameObject InteractionController;
    [SerializeField]
    GameObject InteractionCollider;
    [SerializeField]
    GameObject PlayerController;
    [SerializeField]
    GameObject WardrobeDoors;
    private Vector3 tempRot;
    private float yRot = 0;
    
    float mouseSensitivity = 50f;
    private float xRotation, yRotation;
    private float MouseX, MouseY;
    [SerializeField]
    private GameObject luteString1;
    [SerializeField]
    private GameObject luteString2;
    [SerializeField]
    private GameObject luteString3;
    [SerializeField]
    private GameObject luteString4;
    [SerializeField]
    private GameObject luteString5;
    [SerializeField]
    private GameObject luteString6;

    private List<int> melody;
    private List<string> luteStringsList;
    private bool changeCamera = false;

    private int counter;

    private float labelWidth = Screen.width * 0.06f;
    private float labelHeight = Screen.height * 0.04f;

    private GUIStyle centeredTextStyle;
    [SerializeField]
    GameObject LuteAnimator;

    // Start is called before the first frame update
    void Start()
    {
        LuteAnimator.GetComponent<Animator>().enabled = false;
        SetupMelody();
        SetupLuteStringsList();
        counter = 0;

        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseActivity();
        MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        //UnityEngine.Debug.Log("X: " + MouseX);
        //UnityEngine.Debug.Log("Y: " + MouseY);
        rotator.transform.Rotate(Vector3.up * MouseX);
        camera1.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        tempRot = WardrobeDoors.transform.localRotation.eulerAngles;
        WardrobeDoors.transform.localRotation = Quaternion.Euler(0, Mathf.Lerp(tempRot.y, yRot, 0.4f), 0);
    }
    void OnGUI()
    {
        if (InteractionController.GetComponent<CollectingData>().playingLute)
        {
            //cursor.enabled = true;
            GUI.Box(new Rect(0, Screen.height * 0.8f, Screen.width, Screen.height * 0.2f), "");
            GUI.BeginGroup(new Rect(Screen.width * 0.08f, Screen.height * 0.8f, Screen.width - (Screen.width * 0.08f), Screen.height * 0.2f));
            centeredTextStyle = new GUIStyle("label");
            centeredTextStyle.alignment = TextAnchor.MiddleCenter;
            DisplayCorrectSound();
            GUI.EndGroup();
        }
        else
        {
            //cursor.enabled = false;
        }

    }

    private void CheckPlayingMelody(LuteStringScript luteStringScript)
    {
        if (melody[counter] == luteStringScript.GetId())
        {
            counter++;
            UnityEngine.Debug.Log(counter);
            if (counter == melody.Count)
            {
                UnityEngine.Debug.Log("WIN");
                luteString1.GetComponent<BoxCollider>().enabled = false;
                luteString2.GetComponent<BoxCollider>().enabled = false;
                luteString3.GetComponent<BoxCollider>().enabled = false;
                luteString4.GetComponent<BoxCollider>().enabled = false;
                luteString5.GetComponent<BoxCollider>().enabled = false;
                luteString6.GetComponent<BoxCollider>().enabled = false;
                InteractionCollider.GetComponent<BoxCollider>().enabled = false;
                //camera1.GetComponent<Camera>().enabled = false;
                //camera2.GetComponent<Camera>().enabled = true;
                //changeCamera = true;
                //yRot = 90;
                LuteAnimator.GetComponent<Animator>().enabled = true;
                InteractionController.GetComponent<CollectingData>().playingLute = !InteractionController.GetComponent<CollectingData>().playingLute;

                if (changeCamera)
                {
                    //PlayerController.transform.position = new Vector3(-4.644f, 1, 13.205f);
                    //PlayerController.transform.localRotation = Quaternion.Euler(0, 152.482f, 0);
                    //changeCamera = false;
                }
            }
        }
        else
        {
            counter = 0;
        }
    }

    private void DisplayCorrectSound()
    {
        int stringNumber;
        for (int i = 0; i < counter; i++)
        {
            GUI.Label(new Rect(Screen.width * 0.01f + labelWidth + (i * Screen.width * 0.06f), Screen.height * 0.075f, labelWidth, labelHeight), luteStringsList[melody[i] - 1], centeredTextStyle);
        }
    }

    private void MouseActivity()
    {
        if (Input.GetButtonDown("Interact"))
        {
            LeftMouseClick();
        }
    }

    private void LeftMouseClick()
    {
        LuteStringScript luteStringScript = FindHitObject();

        if (luteStringScript)
        {
            luteStringScript.PlaySound();
            CheckPlayingMelody(luteStringScript);
        }
    }

    private LuteStringScript FindHitObject()
    {
        Ray ray = camera1.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            LuteStringScript temp = hit.collider.gameObject.GetComponent<LuteStringScript>();
            if (temp)
            {
                return temp;
            }
        }
        return null;
    }

    private void SetupMelody()
    {
        melody = new List<int>();
        melody.Add(1);
        melody.Add(2);
        melody.Add(1);
        melody.Add(3);
        melody.Add(2);
        melody.Add(5);
        melody.Add(4);
        melody.Add(6);
        melody.Add(2);
        melody.Add(5);
        melody.Add(1);
    }

    private void SetupLuteStringsList()
    {
        luteStringsList = new List<string>();
        luteStringsList.Add(luteString1.GetComponent<LuteStringScript>().GetStringName());
        luteStringsList.Add(luteString2.GetComponent<LuteStringScript>().GetStringName());
        luteStringsList.Add(luteString3.GetComponent<LuteStringScript>().GetStringName());
        luteStringsList.Add(luteString4.GetComponent<LuteStringScript>().GetStringName());
        luteStringsList.Add(luteString5.GetComponent<LuteStringScript>().GetStringName());
        luteStringsList.Add(luteString6.GetComponent<LuteStringScript>().GetStringName());
    }
    public void DisableStrings()
    {
        luteString1.GetComponent<BoxCollider>().enabled = false;
        luteString2.GetComponent<BoxCollider>().enabled = false;
        luteString3.GetComponent<BoxCollider>().enabled = false;
        luteString4.GetComponent<BoxCollider>().enabled = false;
        luteString5.GetComponent<BoxCollider>().enabled = false;
        luteString6.GetComponent<BoxCollider>().enabled = false;
    }
    public void EnableStrings()
    {
        luteString1.GetComponent<BoxCollider>().enabled = true;
        luteString2.GetComponent<BoxCollider>().enabled = true;
        luteString3.GetComponent<BoxCollider>().enabled = true;
        luteString4.GetComponent<BoxCollider>().enabled = true;
        luteString5.GetComponent<BoxCollider>().enabled = true;
        luteString6.GetComponent<BoxCollider>().enabled = true;
    }
}

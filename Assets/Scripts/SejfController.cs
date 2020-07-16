using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SejfController : MonoBehaviour
{
    [SerializeField]
    private GameObject cylinder1;
    [SerializeField]
    private GameObject cylinder2;
    [SerializeField]
    private GameObject cylinder3;

    [SerializeField]
    private CylinderScript currentCylinder;

    [SerializeField]
    private DoorScript doorScript;
    [SerializeField]
    Animator animator;

    private List<GameObject> cylinderList = new List<GameObject>();
    float mouseSensitivity = 50f;
    private float xRotation, yRotation;
    private float MouseX, MouseY;
    [SerializeField]
    GameObject puzzleCamera;

    // Start is called before the first frame update
    void Start()
    {
        SetupCylinderCollection();
        SetRandomPostionOfCylinders();
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        yRotation += MouseX;
        //puzzleCamera.transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        puzzleCamera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        MouseActivity();
        RotateCylinders();
        if (CheckSafeUnlocked())
        {
            Debug.Log("cylinder1 " + cylinder1.transform.localRotation.eulerAngles + " correct: " + cylinder1.GetComponent<CylinderScript>().GetCorrectX());
            Debug.Log("cylinder2 " + cylinder2.transform.localRotation.eulerAngles + " correct: " + cylinder2.GetComponent<CylinderScript>().GetCorrectX());
            Debug.Log("cylinder3 " + cylinder3.transform.localRotation.eulerAngles + " correct: " + cylinder3.GetComponent<CylinderScript>().GetCorrectX());
            OpenSafe();
            //
        }
        else
        {
            doorScript.SetShouldOpen(false);
        }
    }

    private void MouseActivity()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseClick();
        }
    }

    private void LeftMouseClick()
    {
        CylinderControllerScript cylinderControllerScript = FindHitObject();

        if (cylinderControllerScript)
        {
            CylinderScript tempScript = currentCylinder.GetComponent<CylinderScript>();
            tempScript.ChangeDestinationX(cylinderControllerScript.GetAngle());
        }
    }

    private CylinderControllerScript FindHitObject()
    {
        Ray ray = puzzleCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            CylinderControllerScript temp = hit.collider.gameObject.GetComponent<CylinderControllerScript>();
            if (temp)
            {
                currentCylinder = temp.GetCylinderScript();
                return temp;
            }
        }
        return null;
    }

    private void SetupCylinderCollection()
    {
        cylinderList.Add(cylinder1);
        cylinderList.Add(cylinder2);
        cylinderList.Add(cylinder3);
    }

    private bool CheckSafeUnlocked()
    {
        List<GameObject> tmpList = new List<GameObject>();
        tmpList = cylinderList.FindAll(cyl => cyl.GetComponent<CylinderScript>().IsUnlocked() == true).ToList<GameObject>();
        if (tmpList.Count == cylinderList.Count)
        {
            return true;
        }
        return false;
    }

    private void OpenSafe()
    {
        doorScript.SetShouldOpen(true);
        if (doorScript.ShouldOpen())
        {
            //doorScript.OpenDoor();
            animator.enabled = true;
        }
    }

    private void RotateCylinders()
    {
        for (int i = 0; i < cylinderList.Count; i++)
        {
            GameObject temp = cylinderList[i];
            temp.GetComponent<CylinderScript>().RotateCylinder();
        }
    }

    private void SetRandomPostionOfCylinders()
    {
        while (CheckSafeUnlocked())
        {
            for (int i = 0; i < cylinderList.Count; i++)
            {
                GameObject temp = cylinderList[i];
                temp.GetComponent<CylinderScript>().SetRandomPosition();
            }
        }
    }
}

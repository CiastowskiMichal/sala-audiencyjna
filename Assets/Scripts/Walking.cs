using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;

public class Walking : MonoBehaviour
{
    [SerializeField]
    float speed;
    float mouseSensitivity = 50f;
    private float xMovement, yMovement;
    private float xRotation, yRotation;
    private float MouseX, MouseY;
    Vector3 velocity;
    float gravity = -9.8f;
    public bool showMenu;
    [SerializeField]
    GameObject menuCanvas;
    private bool Crouching;
    private bool canGetUp;

    [SerializeField]
    GameObject cameraController;
    Vector3 move;
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        xMovement = 0;
        yMovement = 0;
        xRotation = 0;
        yRotation = 0;
        showMenu = false;
        menuCanvas.transform.localPosition = new Vector2(0, 720);
        Cursor.lockState = CursorLockMode.Locked;
        move = new Vector3(0, 0, 0);
        Crouching = false;
        cameraController.transform.localPosition = new Vector3(0, 0.65f, 0);
        canGetUp = true;

        //menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Crouching)
        {
            GetComponent<CharacterController>().height = 1;
            cameraController.transform.localPosition = new Vector3(0, 0.3f, 0);
        }
        else if (!Crouching)
        {
            GetComponent<CharacterController>().height = 2;
            cameraController.transform.localPosition = new Vector3(0, 0.65f, 0);
        }
        //Debug.Log(xRotation + ": " + yRotation);
        if (showMenu == false)
        {
            xMovement = Input.GetAxisRaw("Horizontal");
            yMovement = Input.GetAxisRaw("Vertical");
            MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            xRotation -= MouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);
            menuCanvas.transform.localPosition = new Vector2(0, Mathf.Lerp(menuCanvas.transform.localPosition.y, 720, 0.1f));
            Cursor.lockState = CursorLockMode.Locked;
            if (Input.GetButtonDown("Crouch"))
            {
                if(canGetUp)
                Crouching = !Crouching;
            }
        }
        else
        {
            xMovement = 0;
            yMovement = 0;
            MouseX = 0;
            MouseY = 0;
            menuCanvas.transform.localPosition = new Vector2(0, Mathf.Lerp(menuCanvas.transform.localPosition.y, 0, 0.1f));
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetButtonDown("Options"))
        {
            showMenu = !showMenu;
        }

        if (xMovement != 0 || yMovement != 0)
        {
            move = transform.right * xMovement + transform.forward * yMovement;
            controller.Move(move * speed * Time.deltaTime);
        }
        //GetComponent<Rigidbody>().AddRelativeForce(new Vector3(speed * xMovement, 0, speed * yMovement));


        if (MouseX != 0)
            transform.Rotate(Vector3.up * MouseX);

        if (MouseY != 0)
            cameraController.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "CrouchArea")
        {
            //Debug.Log("Pobyt");
            canGetUp = false;
        }
        if(other.transform.tag == "SecondRoom")
        {
            Destroy(GameObject.Find("Dworzanin_Kolizja"));
            Destroy(GameObject.Find("Dworzanin_Model"));
            Destroy(other.gameObject);
        }
    }
    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "CrouchArea")
        {
            //Debug.Log("Wyjscie");
            canGetUp = true;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    public Camera mainCamera;

    public Animator cameraOverlay;

    private GameObject raycastFocus;

    //public Text DebugText;

    private TourController tourController;

    // Use this for initialization
    void Start()
    {
        tourController = GameObject.Find("TourManager").GetComponent<TourController>();
    }

    // called every frame
    void Update()
    {
        castRay();
        checkControllerInput();
    }

    private void castRay()
    {
        RaycastHit hit;
        if (Physics.Raycast (mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit)) {
            if(raycastFocus != null && raycastFocus != hit.collider.gameObject)
            {
                raycastFocus.SendMessage("onRaycastRemoved");
            }
            raycastFocus = hit.collider.gameObject;
            raycastFocus.SendMessage("onRaycastReceived");
        }           
        else
        {
            if (raycastFocus != null)
            {
                raycastFocus.SendMessage("onRaycastRemoved");
                raycastFocus = null;
            }
        }
    }

    private void checkControllerInput()
    {
        bool isAPressed = Input.GetButtonDown("A");
        bool isBPressed = Input.GetButtonDown("B");
        bool isXPressed = Input.GetButtonDown("X");
        bool isYPressed = Input.GetButtonDown("Y");

        //DebugText.text = string.Format("A: {0} B: {1} X: {2} Y: {3}\n", isAPressed, isBPressed, isXPressed, isYPressed);

        if (isAPressed)
        {
            onAPressed();
        }

        if (isBPressed)
        {
            onBPressed();
        }

        if (isXPressed)
        {
            onXPressed();
        }

        if (isYPressed)
        {
            onYPressed();
        }

        if (Input.GetButtonDown("Quit"))
        {
            Application.Quit();
        }
    }

    private void onAPressed()
    {
        //tourController.setCameraMode(true);
        cameraOverlay.SetTrigger("ButtonA");
    }

    private void onBPressed()
    {
        //tourController.setCameraMode(false);
        cameraOverlay.SetTrigger("ButtonB");
    }

    private void onXPressed()
    {

    }

    private void onYPressed()
    {

    }

}
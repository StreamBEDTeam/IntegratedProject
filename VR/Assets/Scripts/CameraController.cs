using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    public Transform rayCastSource;

    public Animator cameraOverlay;

    private GameObject raycastFocus;

    //public Text DebugText;

    private TourController tourController;
    // Use this for initialization
    void Start()
    {
        Debug.Log("Start");
        tourController = GameObject.Find("TourManager").GetComponent<TourController>();
    }

    // called every frame
    void Update()
    {
        //OVRInput.Update();
        castRay();
        checkControllerInput();
    }

    private void FixedUpdate()
    {
        //OVRInput.FixedUpdate();
    }

    private void castRay()
    {
        RaycastHit hit;
        if (Physics.Raycast (rayCastSource.transform.position, rayCastSource.transform.TransformDirection(Vector3.forward), out hit)) {
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
        /*
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("Testing");
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger,
            OVRInput.Controller.RTouch))
        {
            Debug.Log("T1");
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger,
            OVRInput.Controller.RTouch))
        {
            Debug.Log("T2");
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger,
            OVRInput.Controller.LTouch))
        {
            Debug.Log("T3");
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger,
            OVRInput.Controller.Active))
        {
            Debug.Log("T4");
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger,
            OVRInput.Controller.All))
        {
            Debug.Log("T5");
        }
        */

        bool isAPressed = Input.GetButtonDown("A") ||
            (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger,
            OVRInput.Controller.RTouch));
            //indexTrigger.Get();
        bool isBPressed = Input.GetButtonDown("B") ||
            (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger,
            OVRInput.Controller.RTouch));
        //handTrigger.Get();
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
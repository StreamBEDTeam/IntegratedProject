using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    public Camera mainCamera;

    private GameObject raycastFocus;

    public Text DebugText;

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
        bool isAPressed = Input.GetButton("A");
        bool isBPressed = Input.GetButton("B");
        bool isXPressed = Input.GetButton("X");
        bool isYPressed = Input.GetButton("Y");

        DebugText.text = string.Format("A: {0} B: {1} X: {2} Y: {3}\n", isAPressed, isBPressed, isXPressed, isYPressed);

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
    }

    private void onAPressed()
    {
        tourController.setCameraMode(true);
    }

    private void onBPressed()
    {
        tourController.setCameraMode(false);
    }

    private void onXPressed()
    {

    }

    private void onYPressed()
    {

    }

}
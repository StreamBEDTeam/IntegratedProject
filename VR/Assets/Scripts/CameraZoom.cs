using System;
using UnityEngine;
public class CameraZoom : MonoBehaviour
{
    private SnapshotBehaviour snapshotBehaviour;
    public float minFieldOfView = 30f;
    public float maxFieldOfView = 110f;
    [Tooltip("Initial field of view")]
    public float fieldOfView = 50f;
    [Tooltip("Zoom speed/sensitivity")]
    public float sensitivity = 1f;
    [Tooltip("Region where moving the joystick does nothing")]
    public float deadzone = 0.2f;

    private Camera[] cameras;
    private static readonly OVRInput.Axis2D axis = OVRInput.Axis2D.PrimaryThumbstick;
    private static readonly OVRInput.Controller controller = OVRInput.Controller.RTouch;
    private int[] enabledHashes;

    void Start()
    {
        snapshotBehaviour = GameObject.FindObjectOfType<SnapshotBehaviour>();
        enabledHashes = new int[]
        {
            Animator.StringToHash("Opened")
        };
        cameras = GetComponentsInChildren<Camera>();
        if (cameras.Length == 0)
        {
            Debug.LogException(new System.Exception("No cameras"));
        }
    }

    void Update()
    {
        if (Array.IndexOf(enabledHashes, snapshotBehaviour.animator.GetCurrentAnimatorStateInfo(0).shortNameHash) > -1)
        {
            var x = OVRInput.Get(axis, controller).x;
            if (Mathf.Abs(x) > deadzone)
            {
                fieldOfView -= sensitivity * x * Time.deltaTime;
            }
            fieldOfView = Mathf.Clamp(fieldOfView, minFieldOfView, maxFieldOfView);

            foreach (var camera in cameras)
            {
                camera.fieldOfView = fieldOfView;
            }
        }
    }
}

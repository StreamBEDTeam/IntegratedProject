using System;
using UnityEngine;
public class CameraZoom : MonoBehaviour
{
    public SnapshotBehaviour SnapshotBehaviour;
    public float minFieldOfView = 30f;
    public float maxFieldOfView = 110f;
    public float fieldOfView = 50f;
    public float sensitivity = 1f;
    public float deadzone = 0.2f;

    private Camera[] cameras;
    private static readonly OVRInput.Axis2D axis = OVRInput.Axis2D.PrimaryThumbstick;
    private static readonly OVRInput.Controller controller = OVRInput.Controller.RTouch;
    private int[] enabledHashes;
    // Start is called before the first frame update
    void Start()
    {
        if (SnapshotBehaviour == null)
        {
            SnapshotBehaviour = GameObject.FindObjectOfType<SnapshotBehaviour>();
        }
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

    // Update is called once per frame
    void Update()
    {
        if (Array.IndexOf(enabledHashes, SnapshotBehaviour.animator.GetCurrentAnimatorStateInfo(0).shortNameHash) > -1)
        {
            var x = OVRInput.Get(axis, controller).x;
            if (Mathf.Abs(x) > deadzone)
            {
                //Debug.LogFormat("x: {0}->{1} ({2})", x, fieldOfView, Time.deltaTime);
                fieldOfView += sensitivity * x * Time.deltaTime;
            }
            fieldOfView = Mathf.Clamp(fieldOfView, minFieldOfView, maxFieldOfView);

            foreach (var camera in cameras)
            {
                camera.fieldOfView = fieldOfView;
            }
        }
    }
}

using UnityEngine;

public class FaceObject : MonoBehaviour
{
    public bool reverse;
    private OVRCameraRig cam;
    void Start()
    {
        cam = GameObject.FindObjectOfType<OVRCameraRig>();
    }

    void Update()
    {
        var camTransform = cam.centerEyeAnchor;
        if (reverse)
        {
            transform.LookAt(camTransform.transform);
        }
        else
        {
            transform.LookAt((2 * transform.position) - camTransform.position);
        }
    }
}
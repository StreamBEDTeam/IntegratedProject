using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCamera : MonoBehaviour
{
    private OVRCameraRig cam;
    public Vector3 offset;
    void Start()
    {
        cam = GameObject.FindObjectOfType<OVRCameraRig>();
    }

    void Update()
    {
        var eye = cam.centerEyeAnchor;
        transform.position = eye.position +
            (eye.rotation * offset);
//            cam.transform.rot
 //       var camTransform = cam.centerEyeAnchor;
    }
}

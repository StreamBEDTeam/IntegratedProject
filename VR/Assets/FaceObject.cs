using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class FaceObject : MonoBehaviour
{
    //public GameObject targetObject;
    public bool reverse;
    private OVRCameraRig cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindObjectOfType<OVRCameraRig>();
        /*
        c.centerEyeAnchor
            var p = t.GetPose();
            p.position
            targetObject = GameObject.FindObjectOfType<OvrAvatar>().
        }
        */
        
    }

    // Update is called once per frame
    void Update()
    {
        var camTransform = cam.centerEyeAnchor;
        if (reverse)
        {
            //transform.LookAt(transform.position + targetObject.transform.forward);
            transform.LookAt(camTransform.transform);
        }
        else
        {
            transform.LookAt((2 * transform.position) - camTransform.position);
        }
            //transform.position + m_Camera.transform.rotation * Vector3.forward,
            //m_Camera.transform.rotation * Vector3.up);
    }
}
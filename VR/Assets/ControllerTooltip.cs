using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Transform))]
public class ControllerTooltip : MonoBehaviour
{
    public OvrAvatar avatar=null;
    public string TargetName="rctrl:b_trigger";
    private Transform buttonTransform = null;
    private GameObject controller = null;
    private Transform controllerTransform = null;
    private Transform src = null;
    private TextMesh textMesh;
    // Start is called before the first frame update

        public string Text
    {
        get { return textMesh.text; }
        set { textMesh.text = value; }
    }

    private void getTarget()
    {
        if (avatar == null)
        {
            avatar = GameObject.FindObjectOfType<OvrAvatar>();
        }
        if (controller == null && avatar!=null)
        {
            controller = avatar.ControllerRight.gameObject;
            controllerTransform = controller.GetComponent<Transform>();
        }
        if (buttonTransform == null && controller != null)
        {
            foreach (var t in controller.GetComponentsInChildren<Transform>())
            {
                //Debug.Log(t.gameObject.name);
                if (t.gameObject.name == TargetName)
                {
                    //Debug.LogFormat("match {0}", t.gameObject.name);
                    buttonTransform = t;
                    break;
                }
            }
        }
    }

    void Start()
    {
        src = GetComponent<Transform>();
        textMesh = GetComponentInChildren<TextMesh>();

        //var controller = avatar.ControllerRight.gameObject;
        //controller.
        //avatar.ControllerRight
        //controller.
        //var x = controller.gameObject.GetComponent<OvrAvatarComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        getTarget();
        if(buttonTransform != null && controllerTransform != null)
        {
            src.position = buttonTransform.position;
            src.rotation = controllerTransform.rotation;
        }
    }
}

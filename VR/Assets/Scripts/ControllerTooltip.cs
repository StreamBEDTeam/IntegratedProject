using TMPro;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class ControllerTooltip : MonoBehaviour
{
    public OvrAvatar avatar = null;
    public string TargetName = "rctrl:b_trigger";
    private Transform buttonTransform = null;
    private GameObject controller = null;
    private Transform controllerTransform = null;
    private Transform src = null;
    private TextMeshPro textMesh;
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
        if (controller == null && avatar != null)
        {
            controller = avatar.ControllerRight.gameObject;
            controllerTransform = controller.GetComponent<Transform>();
        }
        if (buttonTransform == null && controller != null)
        {
            foreach (var t in controller.GetComponentsInChildren<Transform>())
            {
                if (t.gameObject.name == TargetName)
                {
                    buttonTransform = t;
                    break;
                }
            }
        }
    }

    void Start()
    {
        src = GetComponent<Transform>();
        textMesh = GetComponentInChildren<TextMeshPro>();
    }

    void Update()
    {
        getTarget();
        if (buttonTransform != null && controllerTransform != null)
        {
            src.position = buttonTransform.position;
            src.rotation = controllerTransform.rotation;
        }
    }
}

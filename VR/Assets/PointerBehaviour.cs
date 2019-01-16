using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PointerBehaviour : MonoBehaviour
{
    public MenuButtons Buttons;
    public int SelectedIndex = 0;
    public float smoothTime = 0.1F;

    private RectTransform rectTransform;
    private Vector3 destination;
    private Vector3 velocity = Vector3.zero;
    private Axis2DToPress axis;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SelectedIndex = 0;
        UpdateDestination();
        rectTransform.position = destination;
        axis = new Axis2DToPress(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch, 0.6f);
    }

    void PointerReset()
    {
        SelectedIndex = 0;
        UpdateDestination();
        rectTransform.position = destination;
        foreach (var button in Buttons.buttonBehaviours)
        {
            button.ButtonReset();
        }
    }

    public void OnStateEnter(string stateName)
    {
        if (stateName == "Discarding" || stateName == "Saving")
        {
            PointerReset();
        }
    }

    void UpdateDestination()
    {
        SelectedIndex = (SelectedIndex + Buttons.buttonBehaviours.Length) % (Buttons.buttonBehaviours.Length);
        destination = Buttons.buttonBehaviours[SelectedIndex].Target.position;
    }

    void Update()
    {
        SelectedIndex -= axis.Get();
        if (Input.GetButtonDown("X"))
        {
            SelectedIndex++;
        }
        if (Input.GetButtonDown("Y"))
        {
            SelectedIndex--;
        }
        UpdateDestination();
        if (Input.GetButtonDown("A") ||
            OVRInput.GetDown(
                OVRInput.Button.PrimaryIndexTrigger,
                OVRInput.Controller.RTouch))
        {
            Buttons.buttonBehaviours[SelectedIndex].ButtonClick();
        }
        //rectTransform.position = Vector3.Lerp(rectTransform.position, destination, Speed * Time.deltaTime);
        rectTransform.position = Vector3.SmoothDamp(rectTransform.position, destination, ref velocity, smoothTime);
    }
}

using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PointerBehaviour : MonoBehaviour
{
    public class Axis2DToPress
    {
        private float cutoff;
        private int state;
        private OVRInput.Axis2D axis;
        private OVRInput.Controller controller;
        public Axis2DToPress(OVRInput.Axis2D axis, OVRInput.Controller controller, float cutoff)
        {
            this.axis = axis;
            this.controller = controller;
            this.cutoff = cutoff;
            state = 0;
        }
        public int Get()
        {
            var current = OVRInput.Get(axis, controller).y;//, OVRInput.Controller.RTouch);
            if (current > cutoff)
            {
                if (state == 0)
                {
                    state = 1;
                    return 1;
                }
            }
            else
            {
                if (current < -cutoff)
                {
                    if (state == 0)
                    {
                        state = -1;
                        return -1;
                    }
                }
                else
                {
                    state = 0;
                }
            }
            return 0;
        }

    }
        public IButtonBehaviour[] Buttons;
    public int SelectedIndex = 0;
    public float smoothTime = 0.3F;
    private RectTransform rectTransform;

    private Vector3 destination;
    private Vector3 velocity = Vector3.zero;
    Axis2DToPress axis;
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
        foreach (var button in Buttons)
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
        SelectedIndex = (SelectedIndex + Buttons.Length) % (Buttons.Length);
        destination = Buttons[SelectedIndex].Target.position;
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
        if (Input.GetButtonDown("A") || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger,
            OVRInput.Controller.RTouch))
        {
            Buttons[SelectedIndex].ButtonClick();
        }
        //rectTransform.position = Vector3.Lerp(rectTransform.position, destination, Speed * Time.deltaTime);
        rectTransform.position = Vector3.SmoothDamp(rectTransform.position, destination, ref velocity, smoothTime);
    }
}

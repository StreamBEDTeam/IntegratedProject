using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PointerBehaviour : MonoBehaviour
{
    public IButtonBehaviour[] Buttons;
    public int SelectedIndex = 0;
    public float smoothTime = 0.3F;
    private RectTransform rectTransform;

    private Vector3 destination;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SelectedIndex = 0;
        UpdateDestination();
        rectTransform.position = destination;
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
        if (Input.GetButtonDown("X"))
        {
            SelectedIndex++;
        }
        if (Input.GetButtonDown("Y"))
        {
            SelectedIndex--;
        }
        UpdateDestination();
        if (Input.GetButtonDown("C"))
        {
            Buttons[SelectedIndex].ButtonClick();
        }
        //rectTransform.position = Vector3.Lerp(rectTransform.position, destination, Speed * Time.deltaTime);
        rectTransform.position = Vector3.SmoothDamp(rectTransform.position, destination, ref velocity, smoothTime);
    }
}

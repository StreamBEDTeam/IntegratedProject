using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PointerBehaviour : MonoBehaviour
{
    public FeatureButtonBehaviour[] Buttons;
    public int SelectedIndex;
    public float smoothTime = 0.3F;
    private RectTransform rectTransform;

    private Vector3 destination;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        UpdateDestination();
        rectTransform.position = destination;
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
                Buttons[SelectedIndex].Animator.SetTrigger("Select");
        }
        //rectTransform.position = Vector3.Lerp(rectTransform.position, destination, Speed * Time.deltaTime);
        rectTransform.position = Vector3.SmoothDamp(rectTransform.position, destination, ref velocity, smoothTime);
    }
}

using UnityEngine;
using System;
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Animator))]
public class PointerBehaviour : MonoBehaviour
{
    private Animator _animator;
    public Animator Animator
    {
        get
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            return _animator;
        }
    }
    public MenuButtons Buttons;
 
    public int SelectedIndex
    {
        get {
            return selectedIndex;
        }
        set
        {
            selectedIndex = value;
        }
    }

    public void checkIndex()
    {
        if(Buttons != null)
        {
            selectedIndex = (selectedIndex + Buttons.buttonBehaviours.Length) % (Buttons.buttonBehaviours.Length);
            destination = SelectedButton.gameObject.transform.localPosition+SelectedButton.Target.localPosition;
        }
    }


    public float smoothTime = 0.1F;
    public IButtonBehaviour SelectedButton {
        get
        {
            if (Buttons == null)
            {
                return null;
            }
            else
            {
                return Buttons.buttonBehaviours[SelectedIndex];
            }
        }
    }

    private int selectedIndex = 0;
    private RectTransform rectTransform;
    private Vector3 destination;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SelectedIndex = 0;
        rectTransform.localPosition = destination;
    }

    public void PointerEnabled(bool enabled)
    {
        Animator.SetBool("Enabled", enabled);
    }
    public void PointerClick()
    {
        SelectedButton.ButtonClick();
    }
    public void PointerUp()
    {
        PointerMove(-1);
    }
    public void PointerDown()
    {
        PointerMove(1);
    }
    public void PointerMove(int dir)
    {
        if(dir != 0) { 
            SelectedIndex += dir;
            checkIndex();
        }
    }

    /*
    void PointerReset()
    {
        SelectedIndex = 0;
        UpdateDestination();
        rectTransform.position = destination;
        //foreach (var button in Buttons.buttonBehaviours)
        //{
        //    button.ButtonReset();
        //}
    }
    */

    /*
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
    */

    void Update()
    {
        /*
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
        */
        //rectTransform.position = Vector3.Lerp(rectTransform.position, destination, Speed * Time.deltaTime);
        //rectTransform.position = Vector3.SmoothDamp(rectTransform.position, destination, ref velocity, smoothTime);
        rectTransform.localPosition = Vector3.SmoothDamp(rectTransform.localPosition, destination, ref velocity, smoothTime);
    }
}

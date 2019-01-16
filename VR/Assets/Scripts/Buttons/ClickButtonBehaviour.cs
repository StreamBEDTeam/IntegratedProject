using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class ClickButtonBehaviour : IButtonBehaviour
{
    private Animator animator;
    public UnityEvent clickEvent = new UnityEvent();

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (clickEvent == null)
        {
            clickEvent = new UnityEvent();
        }
    }

    public override void ButtonClick()
    {
        if (IsButtonEnabled) { 
            clickEvent.Invoke();
        }
    }
    public override void ButtonEnabled(bool enabled)
    {
        base.ButtonEnabled(enabled);
        animator.SetBool("Enabled", IsButtonEnabled);
    }
}
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class ClickButtonBehaviour : IButtonBehaviour
{
    private Animator _animator;
    public Animator Animator
    {
        get
        {
            if(_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            return _animator;
        }
    }
    public UnityEvent clickEvent = new UnityEvent();

    private void Start()
    {
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
        Animator.SetBool("Enabled", IsButtonEnabled);
    }
}
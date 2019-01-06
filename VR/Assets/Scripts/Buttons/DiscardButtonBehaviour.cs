using UnityEngine;

public class DiscardButtonBehaviour : IButtonBehaviour
{
    public Animator animator;
    public override void ButtonClick()
    {
        animator.SetTrigger("Discard");
    }

    public override void ButtonReset()
    {
    }
}

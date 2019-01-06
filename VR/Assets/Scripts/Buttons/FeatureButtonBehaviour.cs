using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FeatureButtonBehaviour : IButtonBehaviour
{
    private Animator animator;
    private static readonly string Selected = "Selected";
    void Start()
    {
        animator = GetComponent<Animator>();
        ButtonReset();
    }
    public override void ButtonClick()
    {
        animator.SetBool(Selected, !animator.GetBool(Selected));
    }
    public override void ButtonReset()
    {
        animator.SetBool(Selected, false);
    }
}

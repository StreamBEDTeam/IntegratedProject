using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class FeatureButtonBehaviour : IButtonBehaviour
{
    private Animator animator;
    private static readonly string Selected = "Selected";

    void Start()
    {
        animator = GetComponent<Animator>();
        FeatureName = GetComponentInChildren<Text>().text;
    }
    public override void ButtonClick()
    {
        if (IsButtonEnabled) { 
            animator.SetBool(Selected, !IsSelected);
        }
    }
    public string FeatureName { get; private set; }
    public bool IsSelected
    {
        get { return animator.GetBool(Selected); }
    }
    public override void ButtonEnabled(bool enabled)
    {
        base.ButtonEnabled(enabled);
        animator.SetBool("Enabled", IsButtonEnabled);
        if (!IsButtonEnabled)
        {
            animator.SetBool(Selected, false);
        }
    }
}

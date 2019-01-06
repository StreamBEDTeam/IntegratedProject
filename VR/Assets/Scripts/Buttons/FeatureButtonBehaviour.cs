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
        ButtonReset();
        FeatureName = GetComponentInChildren<Text>().text;
    }
    public override void ButtonClick()
    {
        animator.SetBool(Selected, !IsSelected);
    }
    public override void ButtonReset()
    {
        animator.SetBool(Selected, false);
    }
    public string FeatureName { get; private set; }
    public bool IsSelected
    {
        get { return animator.GetBool(Selected); }
    }
}

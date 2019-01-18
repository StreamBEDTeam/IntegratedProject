using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class FeatureButtonBehaviour : IButtonBehaviour
{
    private Animator animator;
    private static readonly string Selected = "Selected";
    public string FeatureName;
    public FeatureHeader FeatureHeader;

    void Start()
    {
        animator = GetComponent<Animator>();
        //FeatureName = GetComponentInChildren<Text>().text;
    }
    public override void ButtonClick()
    {
        if (IsButtonEnabled) { 
            animator.SetBool(Selected, !IsSelected);
        }
    }
    public bool IsSelected
    {
        get { return animator.GetBool(Selected); }
        set { animator.SetBool(Selected, value); }
    }
    public override void ButtonEnabled(bool enabled)
    {
        base.ButtonEnabled(enabled);
        if (animator == null)
        {
            Debug.LogErrorFormat("Null animator: {0}", name);
        }
        animator.SetBool("Enabled", IsButtonEnabled);
        if (!IsButtonEnabled)
        {
            animator.SetBool(Selected, false);
        }
    }
    public void Incorrect()
    {
        // unselect and shake header
        if (FeatureHeader != null)
        {
            FeatureHeader.Incorrect();
        }
    }
}

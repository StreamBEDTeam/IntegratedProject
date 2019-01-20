using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class FeatureButtonBehaviour : IButtonBehaviour
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
    private static readonly string Selected = "Selected";
    public string FeatureName;
    public FeatureHeader FeatureHeader;

    void Start()
    {
        //FeatureName = GetComponentInChildren<Text>().text;
    }
    public override void ButtonClick()
    {
        if (IsButtonEnabled) { 
            Animator.SetBool(Selected, !IsSelected);
        }
    }
    public bool IsSelected
    {
        get { return Animator.GetBool(Selected); }
        set { Animator.SetBool(Selected, value); }
    }
    public override void ButtonEnabled(bool enabled)
    {
        base.ButtonEnabled(enabled);
        if (Animator == null)
        {
            Debug.LogErrorFormat("Null animator: {0}", name);
        }
        Animator.SetBool("Enabled", IsButtonEnabled);
        if (!IsButtonEnabled)
        {
            Animator.SetBool(Selected, false);
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

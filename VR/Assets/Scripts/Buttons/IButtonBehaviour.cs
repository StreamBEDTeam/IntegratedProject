using UnityEngine;

public abstract class IButtonBehaviour : MonoBehaviour
{
    public abstract void ButtonClick();
    public bool IsButtonEnabled { get; private set; }
    public virtual void ButtonEnabled(bool enabled) { IsButtonEnabled = enabled; }
    public RectTransform Target;
}

using UnityEngine;

public abstract class IButtonBehaviour : MonoBehaviour
{
    public abstract void ButtonClick();
    public abstract void ButtonReset();
    public RectTransform Target;
}

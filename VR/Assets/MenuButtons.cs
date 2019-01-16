using System;
using UnityEngine;
using UnityEngine.Events;

public class MenuButtons : MonoBehaviour
{
    [NonSerialized]
    public IButtonBehaviour[] buttonBehaviours;
    [NonSerialized]
    public FeatureButtonBehaviour[] featureButtons;
    [NonSerialized]
    public SaveButtonBehaviour[] saveButtons;
    [NonSerialized]
    public DiscardButtonBehaviour[] discardButtons;

    public UnityEvent saveButtonEvent = new UnityEvent();
    public UnityEvent discardButtonEvent = new UnityEvent();
 
    void Start()
    {
        buttonBehaviours = GetComponentsInChildren<IButtonBehaviour>(true);
        featureButtons = GetComponentsInChildren<FeatureButtonBehaviour>(true);
        saveButtons = GetComponentsInChildren<SaveButtonBehaviour>(true);
        discardButtons = GetComponentsInChildren<DiscardButtonBehaviour>(true);
        /*
        if (saveButtonEvent == null)
        {
            saveButtonEvent = new UnityEvent();
        }
        if (discardButtonEvent == null)
        {
            discardButtonEvent = new UnityEvent();
        }
        */
        foreach (var button in saveButtons)
        {
            button.clickEvent.RemoveAllListeners();
            button.clickEvent.AddListener(saveButtonEvent.Invoke);
        }
        foreach (var button in discardButtons)
        {
            button.clickEvent.RemoveAllListeners();
            button.clickEvent.AddListener(discardButtonEvent.Invoke);
        }
    }

    public void ButtonsEnabled(bool enable)
    {
        foreach (var button in buttonBehaviours)
        {
            button.ButtonEnabled(enable);
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;

public class MenuButtons : MonoBehaviour
{
    private IButtonBehaviour[] _buttonBehaviours;
    public IButtonBehaviour[] buttonBehaviours
    {
        get
        {
            if (_buttonBehaviours == null)
            {
                _buttonBehaviours = GetComponentsInChildren<IButtonBehaviour>(true);
            }
            return _buttonBehaviours;
        }
    }
    private FeatureButtonBehaviour[] _featureButtons;
    public FeatureButtonBehaviour[] featureButtons
    {
        get
        {
            if (_featureButtons == null)
            {
                _featureButtons = GetComponentsInChildren<FeatureButtonBehaviour>(true);
            }
            return _featureButtons;
        }
    }
    private FeatureHeader[] _featureHeaders;
    public FeatureHeader[] featureHeaders
    {
        get
        {
            if (_featureHeaders == null)
            {
                _featureHeaders = GetComponentsInChildren<FeatureHeader>(true);
            }
            return _featureHeaders;
        }
    }
    [NonSerialized]
    public SaveButtonBehaviour[] saveButtons;
    [NonSerialized]
    public DiscardButtonBehaviour[] discardButtons;

    public UnityEvent saveButtonEvent = new UnityEvent();
    public UnityEvent discardButtonEvent = new UnityEvent();
 
    void Start()
    {
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
        foreach(var header in featureHeaders)
        {
            header.gameObject.SetActive(enable);
        }
    }
}

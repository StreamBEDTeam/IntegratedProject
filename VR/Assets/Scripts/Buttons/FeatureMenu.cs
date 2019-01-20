using System;
using UnityEngine;
public class FeatureMenu : MonoBehaviour
{
    //public SnapshotBehaviour SnapshotBehaviour;
    private MenuButtons _buttons;
    public MenuButtons Buttons
    {
        get
        {
            if (_buttons == null)
            {
                _buttons = GetComponentInChildren<MenuButtons>();
            }
            return _buttons;
        }
    }
    private PointerBehaviour _pointer;
    public PointerBehaviour Pointer
    {
        get
        {
            if (_pointer == null)
            {
                _pointer = GetComponentInChildren<PointerBehaviour>();
            }
            return _pointer;
        }
    }
    public GameObject[] linkedObjects;
    //int[] enabledStates;

    // Start is called before the first frame update
    void Start()
    {
        Pointer.Buttons = Buttons;
    }
    

    // Update is called once per frame
    void Update()
    {
    }
    
    public void MenuEnabled(bool enabled)
    {
        Buttons.ButtonsEnabled(enabled);
        Pointer.PointerEnabled(enabled);
        foreach(var obj in linkedObjects)
        {
            obj.SetActive(enabled);
        }
    }
}

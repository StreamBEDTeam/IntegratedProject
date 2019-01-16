using System;
using UnityEngine;
public class FeatureMenu : MonoBehaviour
{
    //public SnapshotBehaviour SnapshotBehaviour;
    public MenuButtons buttons { get; private set; }
    public PointerBehaviour pointer { get; private set; }
    public GameObject[] linkedObjects;
    //int[] enabledStates;

    // Start is called before the first frame update
    void Start()
    {
        buttons = GetComponentInChildren<MenuButtons>();
        pointer = GetComponentInChildren<PointerBehaviour>();
        pointer.Buttons = buttons;
        //pointer.Buttons = buttons;
        //buttons.discardButtonEvent.AddListener(SnapshotBehaviour.SnapDiscard);
        //buttons.saveButtonEvent.AddListener(SnapshotBehaviour.SnapSave);
        /*
        enabledStates = new int[]
        {
            Animator.StringToHash("Snapped"),
            Animator.StringToHash("Incorrect")
        };
        */
    }

    //public void MenuSelect()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        /*
        if (
            Array.IndexOf(
            enabledStates,
            SnapshotBehaviour.animator.GetCurrentAnimatorStateInfo(0).shortNameHash
            ) > -1)
        {
            MenuEnabled(true);
        }
        else
        {
            MenuEnabled(false);
        }
        */
    }

    /*
    public void OnStateEnter(string stateName)
    {
        if (stateName == "Snapping")
        {
            MenuEnabled(true);
        }
        if(stateName == "Saving")
    }
    */

    public void MenuEnabled(bool enabled)
    {
        buttons.ButtonsEnabled(enabled);
        pointer.PointerEnabled(enabled);
        foreach(var obj in linkedObjects)
        {
            obj.SetActive(enabled);
        }
    }
}

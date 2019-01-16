using System;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [NonSerialized]
    public IButtonBehaviour[] buttonBehaviours;
    [NonSerialized]
    public FeatureButtonBehaviour[] featureButtons;

    void Start()
    {
        buttonBehaviours = GetComponentsInChildren<IButtonBehaviour>(true);
        featureButtons = GetComponentsInChildren<FeatureButtonBehaviour>(true);
    }
}

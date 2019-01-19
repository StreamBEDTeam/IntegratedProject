using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ControllerTooltips : MonoBehaviour
{
    public ControllerTooltip indexTooltip;
    public ControllerTooltip handTooltip;
    public ControllerTooltip thumbTooltip;
    public TooltipsConfig[] configs;
    private SnapshotBehaviour snapshot;

    [Serializable]
    public class TooltipConfig
    {
        public string Text;
        public bool Enabled;

        public void Apply(ControllerTooltip tooltip)
        {
            tooltip.gameObject.SetActive(Enabled);
            tooltip.Text = Text;
        }
    }
    [Serializable]
    public class TooltipsConfig
    {
        public string stateName;
        public TooltipConfig indexConfig;
        public TooltipConfig handConfig;
        public TooltipConfig thumbConfig;

        [NonSerialized]
        public int stateHash;
    }

    // Start is called before the first frame update
    void Start()
    {
        snapshot = GameObject.FindObjectOfType<SnapshotBehaviour>();
        foreach(var config in configs)
        {
            config.stateHash = Animator.StringToHash(config.stateName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var hash = snapshot.animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        foreach(var config in configs)
        {
            if(config.stateHash == hash)
            {
                config.indexConfig.Apply(indexTooltip);
                config.handConfig.Apply(handTooltip);
                config.thumbConfig.Apply(thumbTooltip);
                return;
            }
        }
        // No state
        indexTooltip.gameObject.SetActive(false);
        handTooltip.gameObject.SetActive(false);
        thumbTooltip.gameObject.SetActive(false);
    }
}

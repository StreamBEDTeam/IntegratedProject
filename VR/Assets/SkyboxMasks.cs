using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxMasks : MonoBehaviour
{
    public GlowMask maskPrefab;
    public List<GlowMask> masks;
    public float offsetDelta;
    private SceneConfig sceneConfig;

    void Start()
    {
        sceneConfig = GameObject.FindObjectOfType<SceneConfig>();
        masks = new List<GlowMask>(GetComponentsInChildren<GlowMask>());
        addMasks();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void addMasks() { 
        foreach(var area in sceneConfig.AreaConfigs)
        {
            if (getMask(area.AreaName) == null)
            {
                addMask(area);
            }
        }
    }

    GlowMask getMask(string areaName)
    {
        foreach(var mask in masks)
        {
            if(mask.areaName == areaName)
            {
                return mask;
            }
        }
        return null;
    }

    void addMask(SceneConfig.AreaConfig area)
    {
        ///var mask = GameObject.Instantiate(maskPrefab.gameObject, transform).GetComponent<GlowMask>();
        var mask = GameObject.Instantiate(maskPrefab, transform);
        masks.Add(mask);
        float offset = offsetDelta * masks.Count;
        mask.FromConfig(area, offset);
    }
}

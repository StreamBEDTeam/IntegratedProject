using System;
using UnityEngine;
using UnityEngine.Video;
public class SceneConfig : MonoBehaviour
{
    public VideoClip SkyboxVideoClip;
    public Texture2D MapGraphic;
    public AreaConfig[] AreaConfigs;

    [Serializable]
    public class AreaConfig
    {
        public Texture2D MaskTexture;
        public string AreaName;
        public int AreaType;

        public bool requiredArea;
        public string messageText;
        public string[] correctTags;
    }

    public AreaConfig GetAreaConfig(string areaName)
    {
        foreach (var areaConfig in AreaConfigs)
        {
            if (areaConfig.AreaName == areaName)
            {
                return areaConfig;
            }
        }
        Debug.LogErrorFormat("No configuration for requested area: [{0}]", areaName);
        return null;
    }
}

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
        public string AreaName;
        public int AreaType;
        public Texture2D MaskTexture;

        [Tooltip("True if onboarding area")]
        public bool requiredArea;
        [Tooltip("Message shown (only if onboarding)")]
        public string messageText;
        [Tooltip("Correct features (only if onboarding)")]
        public string[] correctTags;

        public override string ToString()
        {
            return string.Format("Area {0}", AreaName);
        }
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

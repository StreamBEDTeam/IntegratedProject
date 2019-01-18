﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneConfig : MonoBehaviour
{
    public AreaConfig[] AreaConfigs;

    [Serializable]
    public class AreaConfig
    {
        public Material MaskSkybox;
        public RenderTexture TargetTexture;
        public Texture2D MaskTexture;
        public string AreaName;
        public int AreaType;

        public bool requiredArea;
        public string messageText;
        public string[] correctTags;
    }
}

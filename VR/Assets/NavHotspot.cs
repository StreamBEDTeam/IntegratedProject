using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavHotspot : Hotspot
{
    public string sceneName;
    public void Navigate()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    public override void HotspotTrigger()
    {

    }
}

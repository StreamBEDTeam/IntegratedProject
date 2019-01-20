using UnityEngine;
using UnityEngine.SceneManagement;
public class NavHotspot : Hotspot
{
    public string sceneName;
    public override void HotspotTrigger()
    {
        Debug.LogFormat("NavHotspot: {0}", sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}

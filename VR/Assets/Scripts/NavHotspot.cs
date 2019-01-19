using UnityEngine.SceneManagement;

public class NavHotspot : Hotspot
{
    public string sceneName;
    public override void HotspotTrigger()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}

using UnityEngine;
public class QuitHotspot : Hotspot
{
    public override void HotspotTrigger()
    {
        Debug.Log("QuitHotspot");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}

using UnityEngine;

using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class SkyboxRender : MonoBehaviour
{
    VideoPlayer videoPlayer;
    SceneConfig sceneConfig;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        sceneConfig = GameObject.FindObjectOfType<SceneConfig>();
        videoPlayer.clip = sceneConfig.SkyboxVideoClip;
        videoPlayer.Play();
    }
}

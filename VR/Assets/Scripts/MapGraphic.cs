using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class MapGraphic : MonoBehaviour
{
    RawImage rawImage;
    SceneConfig sceneConfig;
    // Start is called before the first frame update
    void Start()
    {
        rawImage = GetComponent<RawImage>();
        sceneConfig = GameObject.FindObjectOfType<SceneConfig>();
        rawImage.texture = sceneConfig.MapGraphic;
    }
}

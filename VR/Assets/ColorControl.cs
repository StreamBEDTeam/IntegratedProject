using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorControl : MonoBehaviour
{
    public Color Color;

    private RawImage[] rawImages;
    private Image[] images;
    // Start is called before the first frame update
    void Start()
    {
        rawImages = GetComponentsInChildren<RawImage>();
        images = GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var img in rawImages)
        {
            img.color = Color;
        }
        foreach (var img in images)
        {
            img.color = Color;
        }
    }
}

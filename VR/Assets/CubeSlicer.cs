using UnityEngine;

public class CubeSlicer : MonoBehaviour
{
    RenderTexture inputTexture;
    Texture2D upperLeft;
    Texture2D upperMiddle;
    Texture2D upperRight;
    Texture2D lowerLeft;
    Texture2D lowerMiddle;
    Texture2D lowerRight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RenderCube()
    {
        int stride = 1024;
        RenderTexture bak = RenderTexture.active;
        upperLeft.ReadPixels(
            new Rect(0* stride, 0 * stride, stride, stride),
            0, 0);
        upperMiddle.ReadPixels(
            new Rect(0 * stride, 0 * stride, stride, stride),
            0, 0);
        upperRight.ReadPixels(
            new Rect(0 * stride, 0 * stride, stride, stride),
            0, 0);
        lowerLeft.ReadPixels(
            new Rect(0 * stride, 0 * stride, stride, stride),
            0, 0);
        lowerMiddle.ReadPixels(
            new Rect(0 * stride, 0 * stride, stride, stride),
            0, 0);
        lowerRight.ReadPixels(
            new Rect(0 * stride, 0 * stride, stride, stride),
            0, 0);

        RenderTexture.active = bak;
    }
}

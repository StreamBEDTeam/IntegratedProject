using System.IO;
using UnityEngine;
using System;
public class ImageUtils
{
    [Serializable]
    public class PixelCount
    {
        public int Total = 0;
        public int Selected = 0;
        public float Covered
        {
            get
            {
                return Selected / ((float)Total);
            }
        }
    }

    public PixelCount CountPixels(Texture2D tex, float cutoff)
    {
        var count = new PixelCount();
        var pix = tex.GetPixels();
        foreach (var c in pix)
        {
            if (c.grayscale >= cutoff)
            {
                count.Selected += 1;
            }
            count.Total += 1;
        }
        return count;
    }


    public void RenderTextureToTexture2D(RenderTexture src, Texture2D dest)
    {
        if (dest.height != src.height || dest.width != src.width)
        {
            Debug.LogErrorFormat("Tex mismatch {0}x{1}->{2}x{3}", src.width, src.height, dest.width, dest.height);
        }

        RenderTexture previouslyActive = RenderTexture.active;
        RenderTexture.active = src;
        dest.ReadPixels(new Rect(0, 0, src.width, src.height), 0, 0);
        RenderTexture.active = previouslyActive;
    }

    public void Texture2DToPng(Texture2D tex, string path)
    {
        var png = tex.EncodeToPNG();
        File.WriteAllBytes(path, png);
    }
}

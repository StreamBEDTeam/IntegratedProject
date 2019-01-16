using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ImageUtils
{
    public class PixelCount
    {
        public int Total = 0;
        public int Selected = 0;
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
        // ReadPixels always reads from RenderTexture.active
        RenderTexture.active = previouslyActive;
    }

    public void Texture2DToPng(Texture2D tex, string path)
    {
        var png = tex.EncodeToPNG();
        File.WriteAllBytes(path, png);
    }

    /*
    public void TextureToPng(RenderTexture src, string path)
    {
        var width = src.width;
        var height = src.height;
        var tex = new Texture2D(width, height);
        RenderTextureToTexture2D(src, tex);
        Texture2DToPng(tex, path);
        UnityEngine.Object.Destroy(tex);
    }
    */
}

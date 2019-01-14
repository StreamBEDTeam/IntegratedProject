using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SaveButtonBehaviour : IButtonBehaviour
{

    public PointerBehaviour Pointer;
    public string SavePath;
    public RenderTexture SourceTexture;
    public Animator animator;
    public CameraZoom zoomer;

    [Serializable]
    public class AreaTexture
    {
        public RenderTexture Texture;
        public Texture2D MaskTexture;
        public string Name;
    }

    public AreaTexture[] AreaTextures;

    private void Start()
    {
        foreach(var tex in AreaTextures)
        {
            var pix = tex.MaskTexture.GetPixels();
            int selected = 0;
            int total = 0;
            foreach(var c in pix)
            {
                if (c.grayscale >= 0.8)
                {
                    selected += 1;
                }
                total += 1;
            }
            Debug.LogFormat("Tex {0}: {1}/{2}", tex.Name, selected, total);
        }
    }

    public override void ButtonClick()
    {
        Directory.CreateDirectory(SavePath);
        var sb = new StringBuilder();
        sb.AppendLine(String.Format("Scene {0}", SceneManager.GetActiveScene().name));
        var dts = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        sb.AppendLine(String.Format("Saved at {0}", dts));
        sb.AppendLine(String.Format("FoV {0}", zoomer.fieldOfView));
        foreach (var button in Pointer.Buttons)
        {
            if (typeof(FeatureButtonBehaviour).IsInstanceOfType(button))
            {
                var but = (FeatureButtonBehaviour)button;
                sb.AppendLine(String.Format(
                    "Feature {0}: {1}",
                    but.FeatureName,
                    but.IsSelected ? "True" : "False"));
            }
        }
        var imgPath = Path.Combine(SavePath, String.Format("{0}.png", dts));
        var txtPath = Path.Combine(SavePath, String.Format("{0}.txt", dts));
        var width = SourceTexture.width;
        var height = SourceTexture.height;

        TextureToPng(SourceTexture, imgPath);

        foreach(var areaTexture in AreaTextures)
        {
            var aPath = Path.Combine(SavePath, String.Format("{0}-{1}.png", dts, areaTexture.Name));
            TextureToPng(areaTexture.Texture, aPath);
        }

        File.WriteAllText(txtPath, sb.ToString());
        animator.SetTrigger("Save");
    }

    private void TextureToPng(RenderTexture src, String path)
    {
        var width = src.width;
        var height = src.height;
        var tex = new Texture2D(width, height);
        RenderTexture previouslyActive = RenderTexture.active;
        RenderTexture.active = src;
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        // ReadPixels always reads from RenderTexture.active
        var png = tex.EncodeToPNG();
        RenderTexture.active = previouslyActive;
        File.WriteAllBytes(path, png);

        // Calculate if in the photo
        int selected=0;
        int count = 0;
        foreach (var pix in tex.GetPixels())
        {
            if (pix.grayscale >= 0.8)
            {
                selected += 1;
            }
            count += 1;
        }
        Debug.LogFormat("Object {0}: {1}/{2}", path, selected, count);
        UnityEngine.Object.Destroy(tex);
        
    }

    public override void ButtonReset()
    {
    }

}

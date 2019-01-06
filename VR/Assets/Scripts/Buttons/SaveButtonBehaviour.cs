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

    public override void ButtonClick()
    {
        Directory.CreateDirectory(SavePath);
        var sb = new StringBuilder();
        sb.AppendLine(String.Format("Scene {0}", SceneManager.GetActiveScene().name));
        var dts = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        sb.AppendLine(String.Format("Saved at {0}", dts));
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
        var tex = new Texture2D(width, height);
        RenderTexture active = RenderTexture.active;
        RenderTexture.active = SourceTexture;
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        RenderTexture.active = active;
        var png = tex.EncodeToPNG();
        UnityEngine.Object.Destroy(tex);
        File.WriteAllBytes(imgPath, png);
        File.WriteAllText(txtPath, sb.ToString());
        animator.SetTrigger("Save");
    }

    public override void ButtonReset()
    {
    }

}

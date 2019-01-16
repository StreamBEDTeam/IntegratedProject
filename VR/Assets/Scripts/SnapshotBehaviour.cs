using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SnapshotBehaviour : MonoBehaviour
{
    public MenuButtons Buttons;
    public string SavePath;
    public Camera snapshotCamera;
    public PhotoCameraArea areaCamera;
    public string snapState;
    public float cutoff;
    public Animator animator;
    public Mask[] Masks;

    [Serializable]
    public class Mask
    {
        public Material MaskSkybox;
        public RenderTexture TargetTexture;
        public Texture2D MaskTexture;
        public string AreaName;

        [System.NonSerialized]
        public ImageUtils.PixelCount MaskCount;
        [System.NonSerialized]
        public ImageUtils.PixelCount SnapCount;
        [System.NonSerialized]
        public Texture2D SaveTexture;
    }
    [System.NonSerialized]
    public float fieldOfView;
    [System.NonSerialized]
    public Texture2D SaveTexture;

    private ImageUtils imageUtils = new ImageUtils();

    private void Start()
    {
        var snapTexture = snapshotCamera.targetTexture;
        SaveTexture = new Texture2D(snapTexture.width, snapTexture.height);
        foreach (var tex in Masks)
        {
            tex.MaskCount = imageUtils.CountPixels(tex.MaskTexture, cutoff);
            tex.SaveTexture = new Texture2D(tex.TargetTexture.width, tex.TargetTexture.height);
            Debug.LogFormat("Tex {0}: {1}/{2}", tex.AreaName, tex.MaskCount.Selected, tex.MaskCount.Total);

        }
    }
    private void OnDestroy()
    {
        if (SaveTexture != null)
        {
            Destroy(SaveTexture);
        }
        foreach (var tex in Masks)
        {
            if (tex.SaveTexture != null)
            {
                Destroy(tex.SaveTexture);
            }
        }
    }
    public void OnStateEnter(string stateName)
    {
        if (stateName == snapState)
        {
            Snap();
        }
    }

    public void Snap()
    {
        snapshotCamera.Render();
        imageUtils.RenderTextureToTexture2D(snapshotCamera.targetTexture, SaveTexture);
        foreach (var mask in Masks)
        {
            areaCamera.Skybox.material = mask.MaskSkybox;
            areaCamera.Camera.targetTexture = mask.TargetTexture;
            areaCamera.Camera.Render();
            imageUtils.RenderTextureToTexture2D(mask.TargetTexture, mask.SaveTexture);
            mask.SnapCount = imageUtils.CountPixels(mask.SaveTexture, cutoff);
        }
        fieldOfView = snapshotCamera.fieldOfView;
    }

    public void SnapSave()
    {
        Directory.CreateDirectory(SavePath);
        var sb = new StringBuilder();
        sb.AppendLine(String.Format("Scene: {0}", SceneManager.GetActiveScene().name));
        var dts = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        sb.AppendLine(String.Format("Save Time: {0}", dts));
        sb.AppendLine(String.Format("FoV: {0}", fieldOfView));
        foreach (var button in Buttons.featureButtons)
        {
            sb.AppendLine(String.Format(
                            "Feature {0}: {1}",
                            button.FeatureName,
                            button.IsSelected ? "True" : "False"));
        }
        foreach (var mask in Masks)
        {
            sb.AppendLine(String.Format(
                "Area {0} Mask Selected: {1}", mask.AreaName, mask.MaskCount.Selected));
            sb.AppendLine(String.Format(
                "Area {0} Mask Total: {1}", mask.AreaName, mask.MaskCount.Total));
            sb.AppendLine(String.Format(
                "Area {0} Snap Selected: {1}", mask.AreaName, mask.SnapCount.Selected));
            sb.AppendLine(String.Format(
                "Area {0} Snap Total: {1}", mask.AreaName, mask.SnapCount.Total));
        }
        var imgPath = Path.Combine(SavePath, String.Format("{0}.png", dts));
        var txtPath = Path.Combine(SavePath, String.Format("{0}.txt", dts));

        imageUtils.Texture2DToPng(SaveTexture, imgPath);

        foreach (var areaTexture in Masks)
        {
            var aPath = Path.Combine(SavePath, String.Format("{0}-{1}.png", dts, areaTexture.AreaName));
            imageUtils.Texture2DToPng(areaTexture.SaveTexture, aPath);
        }

        File.WriteAllText(txtPath, sb.ToString());
        animator.SetTrigger("Save");
    }
}

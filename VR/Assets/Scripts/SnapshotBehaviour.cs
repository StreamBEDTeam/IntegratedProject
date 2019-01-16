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
    public float cutoff;
    public Animator animator;
    public int SelectedArea = -1;
    public float MinSelected = 0.1f;
    public Mask[] Masks;

    public FeatureMenu[] Menus { get; private set; }

    [Serializable]
    public class Mask
    {
        public Material MaskSkybox;
        public RenderTexture TargetTexture;
        public Texture2D MaskTexture;
        public string AreaName;
        public int AreaType;

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

    private int hashOpened;
    private int hashClosed;
    private int hashSnapped;
    private Axis2DToPress axisY;

    private void Start()
    {
        axisY = new Axis2DToPress(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch, 0.6f);
        hashOpened = Animator.StringToHash("Opened");
        hashClosed = Animator.StringToHash("Closed");
        hashSnapped = Animator.StringToHash("Snapped");

        var snapTexture = snapshotCamera.targetTexture;
        SaveTexture = new Texture2D(snapTexture.width, snapTexture.height);
        foreach (var tex in Masks)
        {
            tex.MaskCount = imageUtils.CountPixels(tex.MaskTexture, cutoff);
            tex.SaveTexture = new Texture2D(tex.TargetTexture.width, tex.TargetTexture.height);
            Debug.LogFormat("Tex {0}: {1}/{2}", tex.AreaName, tex.MaskCount.Selected, tex.MaskCount.Total);
        }

        Menus = GetComponentsInChildren<FeatureMenu>();
        foreach(var menu in Menus)
        {
            menu.buttons.saveButtonEvent.RemoveAllListeners();
            menu.buttons.saveButtonEvent.AddListener(SnapSave);
            menu.buttons.discardButtonEvent.RemoveAllListeners();
            menu.buttons.discardButtonEvent.AddListener(SnapDiscard);
        }
    }
    private void Update()
    {
        var primaryIndex = OVRInput.GetDown(
            OVRInput.Button.PrimaryIndexTrigger,
            OVRInput.Controller.RTouch);
        var primaryHand = OVRInput.GetDown(
            OVRInput.Button.PrimaryHandTrigger,
            OVRInput.Controller.RTouch);
        var hash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        var thumbY = axisY.Get();

        if(hash == hashOpened)
        {
            if (primaryIndex)
            {
                Snap();
            }
            else
            {
                if (primaryHand)
                {
                    animator.SetTrigger("Close");
                }
            }
        }
        if(hash == hashClosed)
        {
            if (primaryIndex)
            {
                animator.SetTrigger("Open");
            }
        }
        if(hash == hashSnapped)
        {
            var menu = Menus[Masks[SelectedArea].AreaType];
            if (primaryIndex)
            {
                menu.pointer.PointerClick();
            }
            else { 
                if (primaryHand)
                {
                    SnapDiscard();
                }
                else
                {
                    menu.pointer.PointerMove(-thumbY);
                }
            }
        }
        if (Input.GetButtonDown("Quit"))
        {
            Application.Quit();
        }
    }

    private void OnDestroy()
    {
        if (SaveTexture != null)
        {
            Destroy(SaveTexture);
            SaveTexture = null;
        }
        foreach (var tex in Masks)
        {
            if (tex.SaveTexture != null)
            {
                Destroy(tex.SaveTexture);
                tex.SaveTexture = null;
            }
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
        SelectArea();
        if(SelectedArea >= 0)
        {
            animator.SetTrigger("Snap");
            var area = Masks[SelectedArea];
            var menu = Menus[area.AreaType];
            menu.MenuEnabled(true);
            menu.pointer.SelectedIndex = 0;
            menu.pointer.checkIndex();
        }
        else
        {
            animator.SetTrigger("Miss");
        }
    }

    private void SelectArea()
    {
        SelectedArea = -1;
        float bestSelected = MinSelected;
        for (int i = 0; i < Masks.Length; i++)
        {
            if (Masks[i].SnapCount.Covered >= bestSelected)
            {
                bestSelected = Masks[i].SnapCount.Covered;
                SelectedArea = i;
            }
        }
    }

    public void SnapSave()
    {
        //Todo: Write rotation of user and camera
        Directory.CreateDirectory(SavePath);
        var sb = new StringBuilder();
        sb.AppendLine(String.Format("Scene: {0}", SceneManager.GetActiveScene().name));
        var dts = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        sb.AppendLine(String.Format("Save Time: {0}", dts));
        sb.AppendLine(String.Format("FoV: {0}", fieldOfView));
        sb.AppendLine(String.Format("Selected Area: {0}", Masks[SelectedArea].AreaName));
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

        /*
        foreach (var areaTexture in Masks)
        {
            var aPath = Path.Combine(SavePath, String.Format("{0}-{1}.png", dts, areaTexture.AreaName));
            imageUtils.Texture2DToPng(areaTexture.SaveTexture, aPath);
        }
        */
        var aPath = Path.Combine(SavePath, String.Format("{0}-area-mask.png", dts));
        imageUtils.Texture2DToPng(Masks[SelectedArea].SaveTexture, aPath);

        File.WriteAllText(txtPath, sb.ToString());
        animator.SetTrigger("Save");
        foreach(var menu in Menus)
        {
            menu.MenuEnabled(false);
        }
    }

    public void SnapDiscard()
    {
        animator.SetTrigger("Discard");
        foreach (var menu in Menus)
        {
            menu.MenuEnabled(false);
        }
    }
}

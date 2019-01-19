﻿using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityStandardAssets.Utility;
public class SnapshotBehaviour : MonoBehaviour
{

    public RenderTexture skyboxRenderTextureArea;
    public string SavePath = "Photos";
    public Camera snapshotCamera;
    public PhotoCameraArea areaCamera;
    public float cutoff;
    public Animator animator;
    public float MinSelected = 0.1f;
    public int saveAttemptCount = 0;
    public MessageBehaviour Message;
    public FeatureMenu[] Menus { get; private set; }

    private GameStateHandle gameStateHandle;
    private SceneConfig sceneConfig;

    public SceneConfig.AreaConfig SelectedArea;

    public FeatureMenu SelectedMenu
    {
        get
        {
            if (SelectedArea == null)
            {
                return null;
            }
            else
            {
                return Menus[SelectedArea.AreaType];
            }
        }
    }
    public AreaState SelectedAreaState
    {
        get
        {
            if (SelectedArea == null)
            {
                return null;
            }
            else
            {
                return getAreaState(SelectedArea.AreaName);
            }
        }
    }

    public float fieldOfView;
    public Texture2D SaveTexture;
    public List<AreaState> AreaStates;
    [Serializable]
    public class AreaState
    {
        public string AreaName;
        public ImageUtils.PixelCount MaskCount;
        public ImageUtils.PixelCount SnapCount;
        public Texture2D SaveTexture;
    }
    public AreaState getAreaState(string areaName)
    {
        foreach(var state in AreaStates)
        {
            if(state.AreaName == areaName)
            {
                return state;
            }

        }
        var newState = new AreaState();
        newState.AreaName = areaName;
        AreaStates.Add(newState);
        return newState;
    }

    //[System.NonSerialized]
    //[System.NonSerialized]

    private ImageUtils imageUtils = new ImageUtils();

    private int hashMap;
    private int hashOpened;
    private int hashClosed;
    private int hashSnapped;
    private Axis2DToPress axisY;

    private void Start()
    {
        //shader
        //Skybox s;
        //s.material.shader

        SelectedArea = null;
        AreaStates = new List<AreaState>();
        gameStateHandle = GameObject.FindObjectOfType<GameStateHandle>();
        sceneConfig = GameObject.FindObjectOfType<SceneConfig>();
        axisY = new Axis2DToPress(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch, 0.6f);
        hashMap = Animator.StringToHash("Map");
        hashOpened = Animator.StringToHash("Opened");
        hashClosed = Animator.StringToHash("Closed");
        hashSnapped = Animator.StringToHash("Snapped");

        var snapTexture = snapshotCamera.targetTexture;
        SaveTexture = new Texture2D(snapTexture.width, snapTexture.height);
        foreach (var areaConfig in sceneConfig.AreaConfigs)
        {
            var areaState = getAreaState(areaConfig.AreaName);
            areaState.MaskCount = imageUtils.CountPixels(areaConfig.MaskTexture, cutoff);
            areaState.SaveTexture = new Texture2D(
                areaCamera.Camera.targetTexture.width,
                areaCamera.Camera.targetTexture.height);
            Debug.LogFormat("Tex {0}: {1}/{2}", areaConfig.AreaName, areaState.MaskCount.Selected, areaState.MaskCount.Total);
        }

        Menus = GetComponentsInChildren<FeatureMenu>();
        foreach (var menu in Menus)
        {
            menu.buttons.saveButtonEvent.RemoveAllListeners();
            menu.buttons.saveButtonEvent.AddListener(SnapSave);
            menu.buttons.discardButtonEvent.RemoveAllListeners();
            menu.buttons.discardButtonEvent.AddListener(SnapDiscard);
            menu.MenuEnabled(false);
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

        if (hash == hashMap)
        {
            if (primaryHand)
            {
                animator.SetTrigger("CloseMap");
            }
        }
        if (hash == hashClosed)
        {
            if (primaryIndex)
            {
                animator.SetTrigger("Open");
            }
            else
            {
                if (primaryHand)
                {
                    animator.SetTrigger("OpenMap");
                }
            }
        }
        if (hash == hashOpened)
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
        if (hash == hashSnapped)
        {
            if (primaryIndex)
            {
                SelectedMenu.pointer.PointerClick();
            }
            else
            {
                if (primaryHand)
                {
                    SnapDiscard();
                }
                else
                {
                    SelectedMenu.pointer.PointerMove(-thumbY);
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
        foreach (var tex in AreaStates)
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

        foreach (var areaConfig in sceneConfig.AreaConfigs)
        {
            var areaState = getAreaState(areaConfig.AreaName);
            areaCamera.Skybox.material.SetTexture("_MainTex", areaConfig.MaskTexture);
            //areaCamera.Camera.targetTexture = areaConfig.TargetTexture;
            areaCamera.Camera.Render();
            imageUtils.RenderTextureToTexture2D(areaCamera.Camera.targetTexture, areaState.SaveTexture);
            areaState.SnapCount = imageUtils.CountPixels(areaState.SaveTexture, cutoff);
        }
        fieldOfView = snapshotCamera.fieldOfView;
        SelectArea();
        if (SelectedArea != null)
        {
            SelectedMenu.MenuEnabled(true);
            SelectedMenu.pointer.SelectedIndex = 0;
            SelectedMenu.pointer.checkIndex();
            saveAttemptCount = 0;
            animator.SetTrigger("Snap");
        }
        else
        {
            animator.SetTrigger("Miss");
        }
    }

    private void SelectArea()
    {
        SelectedArea = null;
        float bestSelected = MinSelected;

        foreach(var areaConfig in sceneConfig.AreaConfigs)
        {
            var areaState = getAreaState(areaConfig.AreaName);

            if (areaState.SnapCount.Covered >= bestSelected)
            {
                bestSelected = areaState.SnapCount.Covered;
                SelectedArea = areaConfig;
            }

        }
    }

    public bool checkButton(FeatureButtonBehaviour button)
    {
        //return true if button is correct
        if (button.IsSelected)
        {
            return Array.IndexOf(SelectedArea.correctTags, button.FeatureName) > -1;
        }
        else
        {
            return Array.IndexOf(SelectedArea.correctTags, button.FeatureName) == -1;
        }
    }

    public bool checkFeatures()
    {
        // return true if all buttons are correct
        var area = SelectedArea;
        var menu = SelectedMenu;
        var correct = true;
        if (area.requiredArea)
        {
            foreach (var button in menu.buttons.featureButtons)
            {
                if (!checkButton(button))
                {
                    correct = false;
                    if (saveAttemptCount > 0)
                    {
                        button.Incorrect();
                    }
                }
            }
            if (!correct)
            {
                foreach (var ib in menu.buttons.featureButtons)
                {
                    ib.IsSelected = false;
                }
            }
        }
        return correct;
    }

    public void SnapSave()
    {
        if (!checkFeatures())
        {
            saveAttemptCount += 1;
            animator.SetTrigger("Incorrect");
            return;
        }
        //Todo: Write rotation of user and camera
        Directory.CreateDirectory(SavePath);
        var sb = new StringBuilder();
        sb.AppendLine(String.Format("Scene: {0}", SceneManager.GetActiveScene().name));
        var dts = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        sb.AppendLine(String.Format("Save Time: {0}", dts));
        sb.AppendLine(String.Format("FoV: {0}", fieldOfView));
        sb.AppendLine(String.Format("Selected Area: {0}", SelectedArea.AreaName));
        //var area = Masks[SelectedAreaId];
        //var menu = Menus[area.AreaType];
        foreach (var button in SelectedMenu.buttons.featureButtons)
        {
            sb.AppendLine(String.Format(
                            "Feature {0}: {1}",
                            button.FeatureName,
                            button.IsSelected ? "True" : "False"));
        }
        foreach (var areaConfig in sceneConfig.AreaConfigs)
        {
            var areaState = getAreaState(areaConfig.AreaName);
            sb.AppendLine(String.Format(
                "Area {0} Mask Selected: {1}", areaState.AreaName, areaState.MaskCount.Selected));
            sb.AppendLine(String.Format(
                "Area {0} Mask Total: {1}", areaState.AreaName, areaState.MaskCount.Total));
            sb.AppendLine(String.Format(
                "Area {0} Snap Selected: {1}", areaState.AreaName, areaState.SnapCount.Selected));
            sb.AppendLine(String.Format(
                "Area {0} Snap Total: {1}", areaState.AreaName, areaState.SnapCount.Total));
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
        imageUtils.Texture2DToPng(SelectedAreaState.SaveTexture, aPath);

        File.WriteAllText(txtPath, sb.ToString());
        foreach (var m in Menus)
        {
            m.MenuEnabled(false);
        }
        gameStateHandle.GameState.SetIsCaptured(SelectedArea.AreaName, true);
        if (SelectedArea.requiredArea)
        {
            Message.Text = SelectedArea.messageText;
            animator.SetTrigger("Correct");
        }
        else
        {
            animator.SetTrigger("Save");
        }
    }

    public void SnapDiscard()
    {
        foreach (var menu in Menus)
        {
            menu.MenuEnabled(false);
        }
        animator.SetTrigger("Discard");
    }
}

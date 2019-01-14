using UnityEngine;
using System;
public class SnapshotBehaviour : MonoBehaviour
{
    public Camera snapshotCamera;
    public PhotoCameraArea areaCamera;
    public string snapState;
    [Serializable]
    public class Mask
    {
        public Material MaskSkybox;
        public RenderTexture TargetTexture;
    }
    public Mask[] Masks;

    private void Start()
    {
        
    }

    public void OnStateEnter(string stateName)
    {
        if (stateName == snapState)
        {
            snapshotCamera.Render();
            foreach(var mask in Masks)
            {
                areaCamera.Skybox.material = mask.MaskSkybox;
                areaCamera.Camera.targetTexture = mask.TargetTexture;
                areaCamera.Camera.Render();
            }
        }
    }
}

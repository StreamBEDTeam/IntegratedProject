using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RawImage))]
public class GlowMask : MonoBehaviour
{
    public string areaName;
    public float offset;
    public int areaType;
    public bool isCaptured;
    public Texture2D maskTexture;

    GameStateHandle gameStateHandle;
    Animator animator;
    RawImage image;

    void Start()
    {
        gameStateHandle = GameObject.FindObjectOfType<GameStateHandle>();
        animator = GetComponent<Animator>();
        image = GetComponent<RawImage>();
        image.texture = this.maskTexture;
        animator.SetFloat("Offset", offset);
        animator.SetInteger("AreaType", areaType);
        animator.SetBool("Captured", isCaptured);
    }
    
    void Update()
    {
        isCaptured = gameStateHandle.Instance.GetIsCaptured(areaName);
        animator.SetBool("Captured", isCaptured);
    }

    public void FromConfig(SceneConfig.AreaConfig area, float offset)
    {
        areaName = area.AreaName;
        areaType = area.AreaType;
        gameObject.name = string.Format("Mask {0}", area.AreaName);
        maskTexture = area.MaskTexture;
        this.offset = offset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public abstract class Hotspot : MonoBehaviour
{
    private Animator animator;
    private GameStateHandle gameState;
    public int UnlockCount;
    public float Speed=1f;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        gameState = GameObject.FindObjectOfType<GameStateHandle>();
        animator.SetFloat("Speed", Speed);
    }
    public void OnStateEnter(string state)
    {
        if (state == "Trigger")
        {
            HotspotTrigger();
        }
    }
    public void RaycastHit(bool isHit)
    {
        animator.SetBool("IsFocus", isHit);
    }
    public abstract void HotspotTrigger();

    public bool HotspotEnabled
    {
        get
        {
            return animator.GetBool("Enabled");
        }
        set
        {
            animator.SetBool("Enabled", value);
        }
    }

    private void Update()
    {
        var state = gameState.GameState;
        var sceneState = state.getSceneState();
        HotspotEnabled = sceneState.CapturedAreaCount >= UnlockCount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Animator))]
public class GlowMask : MonoBehaviour
{
    public string areaName;
    public bool isStream;
    public bool isCaptured;
    Animator animator;
    GameStateHandle gameStateHandle;
    // Start is called before the first frame update
    void Start()
    {
        gameStateHandle = GameObject.FindObjectOfType<GameStateHandle>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsStream", isStream);
    }

    // Update is called once per frame
    void Update()
    {
        isCaptured = gameStateHandle.GameState.GetIsCaptured(areaName);
        animator.SetBool("Captured", isCaptured);
    }
}

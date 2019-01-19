using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirePhotos : MonoBehaviour
{
    private GameStateHandle gameState;
    private GameObject targetObject;
    public int UnlockCount;

    private void Start()
    {
        gameState = GameObject.FindObjectOfType<GameStateHandle>();
        targetObject = transform.GetChild(0).gameObject;
    }
    
    private void Update()
    {
        var state = gameState.Instance;
        var sceneState = state.getSceneState();
        var active = sceneState.CapturedAreaCount >= UnlockCount;
        targetObject.SetActive(active);
    }
}

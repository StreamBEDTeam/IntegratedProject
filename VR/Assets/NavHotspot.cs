using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class NavHotspot : MonoBehaviour
{
    public string sceneName;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Navigate()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    public void OnStateEnter(string state)
    {
        if(state == "Warp")
        {
            Debug.Log("Warping");
            //Navigate();
        }
    }
    public void RaycastHit(bool isHit)
    {
        animator.SetBool("IsFocus", isHit);
    }
}

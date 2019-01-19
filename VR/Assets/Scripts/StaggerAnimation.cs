using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaggerAnimation : MonoBehaviour
{
    public Animator[] animators;
    public float offsetDelta;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float offset = 0;
        foreach(var anim in animators)
        {
            anim.SetFloat("Offset", offset);
            offset += offsetDelta;
        }
    }
}

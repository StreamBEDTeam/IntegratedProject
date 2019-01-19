using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AreaMasks : MonoBehaviour
{
    [Serializable]
    public class AreaMask 
    {
        public Texture Texture;
        public string Name;
    }
    public AreaMask[] Masks;

    // Start is called before the first frame update
    void Start()
    {
        
    }
}

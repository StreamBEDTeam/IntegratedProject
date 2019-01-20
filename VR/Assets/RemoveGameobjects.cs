using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGameobjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Remove<ArduinoInstance>();
        Remove<GameStateInstance>();
    }

    void Remove<T>() where T:MonoBehaviour
    {
        var objects = GameObject.FindObjectsOfType<T>();
        foreach(var o in objects)
        {
            GameObject.Destroy(o.gameObject);
        }
    }
}

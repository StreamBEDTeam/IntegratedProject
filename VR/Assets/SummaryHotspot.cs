using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryHotspot : Hotspot
{
    public GameObject[] LinkedObjects;
    public override void HotspotTrigger()
    {
        foreach (var o in LinkedObjects)
        {
            o.SetActive(true);
        }
        this.gameObject.SetActive(false);
    }
}

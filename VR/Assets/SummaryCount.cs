using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform = GetComponent<RectTransform>();
        Text = GetComponentInChildren<Text>();
    }

    public RectTransform RectTransform { get; private set; }
    public Text Text { get; private set; }
}

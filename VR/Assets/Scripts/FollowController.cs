using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class FollowController : MonoBehaviour
{
    private Transform target;
    private Transform source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<Transform>();
        target = GameObject.FindObjectOfType<OvrAvatar>().ControllerRight.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        source.transform.position = target.position;
        source.transform.rotation = target.rotation;
    }
}

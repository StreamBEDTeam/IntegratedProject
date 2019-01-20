using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using System;
using UnityEditorInternal;
//[RequireComponent(typeof(OVRPhysicsRaycaster))]
[RequireComponent(typeof(LineRenderer))]
public class FingerPointer : MonoBehaviour
{
    private OvrAvatar avatar;
    //private Transform transform;
    private LineRenderer line;
    //private OVRPhysicsRaycaster raycaster;
    private EventSystem eventSystem;
    private SnapshotBehaviour snapshot;
    // Start is called before the first frame update
    public MaskAttribute RaycastMask;
    public string ActiveState = "Closed";
    public float pointerLen = 0.5f;
    private int activeStateHash;

    private Hotspot selectedHotspot=null;

void Start()
    {
        //transform = GetComponent<Transform>();
        line = GetComponent<LineRenderer>();
        //raycaster = GetComponent<OVRPhysicsRaycaster>();
        //eventSystem = GameObject.FindObjectOfType<EventSystem>();
        avatar = GameObject.FindObjectOfType<OvrAvatar>();
        snapshot = GameObject.FindObjectOfType<SnapshotBehaviour>();
        activeStateHash = Animator.StringToHash(ActiveState);
        Debug.LogFormat("Raycast mask: {0}", RaycastMask.mask);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 fw;
        //Vector3 up;
        //avatar.GetPointingDirection(OvrAvatar.HandType.Right, ref fw, ref up);
        //avatar.ControllerRight.transform
        //var t = avatar.GetHandTransform(OvrAvatar.HandType.Right, OvrAvatar.HandJoint.IndexTip);
        if (snapshot.animator.GetCurrentAnimatorStateInfo(0).shortNameHash == activeStateHash)
        {
            line.enabled = true;
            var t = avatar.ControllerRight.transform;
            transform.position = t.position;
            transform.rotation = t.rotation;
            var pos1 = t.position;
            var pos2 = t.position + (t.forward * pointerLen);
            RaycastHit hit;
            if (Physics.Raycast(t.position, t.forward, out hit, Mathf.Infinity, RaycastMask.mask))
            {
                //Debug.LogFormat("Raycasted {0}", hit.collider.gameObject.name);
                pos2 = hit.point;
                line.startColor = Color.grey;
                line.endColor = Color.grey;

                var hotspot = hit.collider.gameObject.GetComponent<Hotspot>();
                if(selectedHotspot != null && selectedHotspot != hotspot)
                {
                    selectedHotspot.RaycastHit(false);
                    selectedHotspot = null;
                }               

                if (hotspot != null) {
                    selectedHotspot = hotspot;
                    selectedHotspot.RaycastHit(true);
                    if (selectedHotspot.HotspotEnabled)
                    {
                        line.startColor = Color.red;
                        line.endColor = Color.red;
                    }
                }
            }
            else
            {
                if (selectedHotspot != null)
                {
                    selectedHotspot.RaycastHit(false);
                    selectedHotspot = null;
                }
                line.startColor = Color.white;
                line.endColor = Color.white;
            }
            line.SetPositions(new Vector3[] { pos1, pos2 });
        }
        else
        {
            line.enabled = false;
        }
    }
}

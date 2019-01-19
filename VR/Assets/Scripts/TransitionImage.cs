using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FaceObject))]
[RequireComponent(typeof(TrackCamera))]
[RequireComponent(typeof(Animator))]
public class TransitionImage : MonoBehaviour
{
    private Animator animator;
    private SnapshotBehaviour snapshot;
    public string activeStateName="Map";
    private int activeStateHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        snapshot = GameObject.FindObjectOfType<SnapshotBehaviour>();
        activeStateHash = Animator.StringToHash(activeStateName);
    }

    public void EnableImage(bool enabled)
    {
        animator.SetBool("Enabled", enabled);
    }
    private void Update()
    {
        var enabled = snapshot.animator.GetCurrentAnimatorStateInfo(0).shortNameHash == activeStateHash;
        EnableImage(enabled);
    }
}

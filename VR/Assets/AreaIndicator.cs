using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AreaIndicator : MonoBehaviour
{
    SnapshotBehaviour snapshot;
    int activeHash;
    Animator animator;
    void Start()
    {
        snapshot = GameObject.FindObjectOfType<SnapshotBehaviour>();
        activeHash = Animator.StringToHash("Snapped");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(
            "Active",
            snapshot.animator.GetCurrentAnimatorStateInfo(0).shortNameHash == activeHash);
    }
}

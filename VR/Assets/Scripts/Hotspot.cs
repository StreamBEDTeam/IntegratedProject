using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Hotspot : MonoBehaviour
{
    private Animator animator;
    private GameStateHandle gameState;
    private SnapshotBehaviour snapshot;
    public int UnlockCount;
    public float Speed = 1f;
    private int activeHash;

    private void Start()
    {
        animator = GetComponent<Animator>();
        snapshot = GameObject.FindObjectOfType<SnapshotBehaviour>();
        gameState = GameObject.FindObjectOfType<GameStateHandle>();
        animator.SetFloat("Speed", Speed);
        activeHash = Animator.StringToHash("Closed");
    }
    public void OnStateEnter(string state)
    {
        if (state == "Trigger")
        {
            HotspotTrigger();
        }
    }
    public void RaycastHit(bool isHit)
    {
        animator.SetBool("IsFocus", isHit);
    }
    public abstract void HotspotTrigger();

    public bool HotspotEnabled
    {
        get
        {
            return animator.GetBool("Enabled") && !animator.GetBool("Hidden");
        }
    }

    private void Update()
    {
        var state = gameState.GameState;
        var sceneState = state.getSceneState();
        animator.SetBool("Enabled", sceneState.CapturedAreaCount >= UnlockCount);
        animator.SetBool("Hidden", snapshot.animator.GetCurrentAnimatorStateInfo(0).shortNameHash != activeHash);
    }
}

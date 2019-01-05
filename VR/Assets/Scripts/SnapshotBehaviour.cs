using UnityEngine;

[RequireComponent(typeof(Animation))]
public class SnapshotBehaviour : MonoBehaviour
{
    //public RenderTexture snapshotTexture;
    public Camera PhotoCamera;

    private Animation anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Take the snapshot
    public void OnSnapping()
    {
        PhotoCamera.Render();
    }
}

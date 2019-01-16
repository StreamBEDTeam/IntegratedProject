using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SaveButtonBehaviour : IButtonBehaviour
{
    public SnapshotBehaviour snapshot;

    public override void ButtonClick()
    {
        snapshot.SnapSave();
    }

    public override void ButtonReset()
    {
    }
}
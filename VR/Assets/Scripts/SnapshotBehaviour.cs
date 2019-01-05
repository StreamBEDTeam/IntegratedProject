using UnityEngine;

public class SnapshotBehaviour : MonoBehaviour
{
    public Camera PhotoCamera;
    public string TriggerState;

    public void OnStateEnter(string stateName)
    {
        if (stateName == TriggerState)
        {
            PhotoCamera.Render();
        }
    }
}

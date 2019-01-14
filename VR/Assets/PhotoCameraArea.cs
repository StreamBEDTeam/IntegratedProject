using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(Skybox))]
public class PhotoCameraArea : MonoBehaviour
{
    void Start()
    {
        Camera = GetComponent<Camera>();
        Skybox = GetComponent<Skybox>();
    }

    public Camera Camera { get; private set; }
    public Skybox Skybox { get; private set; }
}

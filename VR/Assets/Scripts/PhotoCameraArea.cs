using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(Skybox))]
public class PhotoCameraArea : MonoBehaviour
{
    private Camera camera;
    private Skybox skybox;

    public Camera Camera
    {
        get
        {
            if (camera == null)
            {
                camera = GetComponent<Camera>();
            }
            return camera;
        }
    }
    public Skybox Skybox
    {
        get
        {
            if (skybox == null)
            {
                skybox = GetComponent<Skybox>();
            }
            return skybox;
        }
    }
}

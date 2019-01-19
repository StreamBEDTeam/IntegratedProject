using UnityEngine;

public class AvatarMaterialFix : MonoBehaviour
{
    OvrAvatar avatar;
    private SkinnedMeshRenderer mesh = null;
    private bool isFixed = false;
    public Shader shader;
    void Start()
    {
        avatar = GetComponent<OvrAvatar>();
        if (avatar == null)
        {
            avatar = GameObject.FindObjectOfType<OvrAvatar>();
        }
        if (avatar == null)
        {
            Debug.LogError("No OvrAvatar found");
        }
    }

    void Update()
    {
        if (!isFixed)
        {
            if (
                FixObject(avatar.HandRight.gameObject) &&
            FixObject(avatar.ControllerRight.gameObject))
            {
                isFixed = true;
            }
        }
    }

    bool FixObject(GameObject obj)
    {
        bool ret = false;
        foreach (var o in obj.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            foreach (var mat in o.materials)
            {
                mat.shader = shader;
                ret = true;
            }
        }
        return ret;
    }
}

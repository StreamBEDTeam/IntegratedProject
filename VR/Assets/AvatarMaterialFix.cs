using UnityEngine;

//[RequireComponent(typeof(OvrAvatar))]
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
        if(avatar == null)
        {
            Debug.LogError("No OvrAvatar found");
        }
    }
    
    void Update()
    {
        if (!isFixed)
        {
            FixObject(avatar.HandRight.gameObject);
            FixObject(avatar.ControllerRight.gameObject);
            isFixed = true;
        }
    }

    void FixObject(GameObject obj)
    {
        foreach(var o in obj.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            foreach(var mat in o.materials)
            {
                mat.shader = shader;
            }
        }
    }
}

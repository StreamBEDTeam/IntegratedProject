using UnityEngine;

[RequireComponent(typeof(OvrAvatar))]
public class AvatarMaterialFix : MonoBehaviour
{
    OvrAvatar avatar;
    private SkinnedMeshRenderer mesh = null;
    private bool isFixed = false;
    public Shader shader;
    // Start is called before the first frame update
    void Start()
    {
        avatar = GetComponent<OvrAvatar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFixed)
        {
            if (mesh == null){
                mesh = avatar.HandRight.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            }
            if (mesh != null)
            {
                mesh.material.shader = shader;
                isFixed = true;
            }
        }
    }
}

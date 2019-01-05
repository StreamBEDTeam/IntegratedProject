using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FeatureButtonBehaviour : MonoBehaviour
{
    public RectTransform Target;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public Animator Animator
    {
        get
        {
            return animator;
        }
    }
}

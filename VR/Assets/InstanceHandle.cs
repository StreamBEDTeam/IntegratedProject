using UnityEngine;
public abstract class InstanceHandle<T> : MonoBehaviour where T : MonoBehaviour
{
    public T prefabInstance;
    public T Instance
    {
        get
        {
            makeInstance();
            return instance;
        }
    }
    private T instance;
    private void makeInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<T>();
        }
        if (instance == null)
        {
            instance = Instantiate(prefabInstance);
            DontDestroyOnLoad(instance.gameObject);
        }

    }

    void Start()
    {
        makeInstance();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class LoadingCircleUIController : MonoBehaviour {

    private float duration;

    private float t;

    private bool isLoading;

    public Image loadingImage;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		if (isLoading)
        {
            t += Time.deltaTime / duration;
            float progress = Mathf.Lerp(0, 1, t);
            loadingImage.fillAmount = progress;
        }
        else
        {
            loadingImage.fillAmount = 0f;
        }
	}

    public void StartLoading(float duration)
    {
        this.duration = duration;
        this.t = 0;
        isLoading = true;
    }

    public void StopLoading()
    {
        isLoading = false;
        this.duration = 0f;
    }
}

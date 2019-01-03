using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HotspotController : MonoBehaviour
{

    private bool isObserved = false;

    public UnityEngine.Video.VideoPlayer videoPlayer;

    private IEnumerator coroutine;

    public string sceneToLoad;

    private TourController tourController;

    private LoadingCircleUIController loadingUiController;

    // Amount of time the hotspot has to be looked at before changing scenes/videos
    private int GAZE_THRESHOLD_SECONDS = 3;

    // Use this for initialization
    void Start()
    {
        tourController = GameObject.Find("TourManager").GetComponent<TourController>();
        loadingUiController = GameObject.Find("Canvas").GetComponent<LoadingCircleUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onRaycastReceived() {
        if (!isObserved)
        {
            coroutine = ChangeVideoAfterThreshold(GAZE_THRESHOLD_SECONDS);
            StartCoroutine(coroutine);
            loadingUiController.StartLoading(GAZE_THRESHOLD_SECONDS);
            isObserved = true;
        }
    }

    public void onRaycastRemoved()
    {
        if (isObserved)
        {
            StopCoroutine(coroutine);
            loadingUiController.StopLoading();
            isObserved = false;
        }
    }

    private IEnumerator ChangeVideoAfterThreshold (int waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        if (isObserved)
        {
            changeScene();
        }
    }

    private void changeScene()
    {
        tourController.LoadScene(sceneToLoad);
    }

}
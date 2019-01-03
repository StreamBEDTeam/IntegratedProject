using UnityEngine;
using UnityEngine.SceneManagement;

public class TourController : MonoBehaviour
{

    public GameObject cameraPanel;

    public void setCameraMode(bool isEnabled)
    {
        Debug.Log("Setting camera mode: " + isEnabled);
        cameraPanel.SetActive(isEnabled);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

}
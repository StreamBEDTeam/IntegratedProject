using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArduinoInstance : MonoBehaviour
{
    ArduinoSerial arduinoSerial = null;

    void Start()
    {
        arduinoSerial = new ArduinoSerial();
        arduinoSerial.ConnectToArduino(baudRate: 9600);
        AnnounceScene(SceneManager.GetActiveScene());
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        AnnounceScene(arg0);
    }

    private void AnnounceScene(Scene scene)
    {
        arduinoSerial.SendText(string.Format("scene={0}\n", scene.name));
    }

    private void Cleanup()
    {
        if (arduinoSerial != null)
        {
            arduinoSerial.ClosePort();
            arduinoSerial = null;
        }
    }

    private void OnDestroy()
    {
        Cleanup();
    }

    void OnApplicationQuit()
    {
        Cleanup();
    }

    /*
    void Update()
    {
        Debug.Log("someInt: " + arduinoSerial.ReadInput("someInt"));
        Debug.Log("someFloat: " + arduinoSerial.ReadInput("someFloat"));
        Debug.Log("someString: " + arduinoSerial.ReadInput("someString"));
    }
    */

}

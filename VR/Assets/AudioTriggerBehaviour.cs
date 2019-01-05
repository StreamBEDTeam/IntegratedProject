using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioTriggerBehaviour : MonoBehaviour
{
    AudioSource audioSource;
    public string TriggerState;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnStateEnter(string stateName)
    {
        if (stateName == TriggerState)
        {
            audioSource.Play();
        }
    }
}

using UnityEngine;

public class StartAllAudio : MonoBehaviour
{
    void Start()
    {
        foreach (AudioSource audio in GetComponentsInChildren<AudioSource>())
            audio.Play();
    }
}

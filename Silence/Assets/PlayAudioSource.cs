using UnityEngine;

public class PlayAudioSource : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayAudio()
    {
        GetComponent<AudioSource>().Play();
    }
}

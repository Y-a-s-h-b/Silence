using UnityEngine;

public class AudioTesting : MonoBehaviour
{
    public int sampleSize = 1024; // Number of audio samples to analyze
    public float audioLevel;     // The calculated audio level (RMS)

    private float[] audioSamples;

    void Start()
    {
        audioSamples = new float[sampleSize];
    }

    void Update()
    {
        // Get audio samples from the AudioListener
        AudioListener.GetOutputData(audioSamples, 0);

        // Calculate RMS (Root Mean Square) to represent the audio level
        float sum = 0f;
        for (int i = 0; i < sampleSize; i++)
        {
            sum += audioSamples[i] * audioSamples[i];
        }
        audioLevel = Mathf.Sqrt(sum / sampleSize);

        // Log or use the audio level
        Debug.Log($"Audio Level: {audioLevel}");
    }
}

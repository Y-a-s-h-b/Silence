using MoreMountains.CorgiEngine;
using UnityEngine;

public class AudioTesting : MonoBehaviour
{
    public float audioLevel; // Audio level in dB
    private Health health;
    private const int sampleSize = 1024; // Number of audio samples to analyze
    private float[] samples;

    void Start()
    {
        samples = new float[sampleSize];
        health = GetComponent<Health>();
    }

    void Update()
    {
        // Get audio data from the listener
        AudioListener.GetOutputData(samples, 0);

        // Calculate RMS (Root Mean Square) value of the audio samples
        float rms = 0f;
        foreach (float sample in samples)
        {
            rms += sample * sample;
        }
        rms = Mathf.Sqrt(rms / sampleSize);

        // Convert RMS to decibels (dB)
        audioLevel = rms > 0 ? 20f * Mathf.Log10(rms) : -74.8f; // -80 dB is silence
        audioLevel = Mathf.Clamp(audioLevel, -74.8f, 0f); // Clamp to a reasonable range

        float n = ((audioLevel + 74.8f) / 74.8f) * 100f;
        Debug.Log($"Audio Level: {audioLevel:F2} dB" + n);
        health.SetHealth(Mathf.RoundToInt(n) + 10, gameObject);        
    }
}

using MoreMountains.CorgiEngine;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTesting : MonoBehaviour
{
    public float audioLevel; // Audio level in dB
    public AudioMixer audioMixer;
    public AudioMixerGroup amg;
    private Health health;
    private const int sampleSize = 1024; // Number of audio samples to analyze
    private float[] samples;
    private float smoothedValue;
    private float currentValue;
    public float lerpSpeed = 0.05f;
    
    void Start()
    {
        samples = new float[sampleSize];
        health = GetComponent<Health>();
        smoothedValue = 0f;
        float val = 0;
        //audioMixer.GetFloat("SfxVolume" ,out val);
        

        //float sfxGroup = audioMixer.GetFloat()
        
    }

    void Update()
    {
        float val = 0;
        audioMixer.GetFloat("SfxVolume", out val);
        //Debug.Log("value vol" + val);
        // Get audio data from the listener
        AudioListener.GetOutputData(samples, 0);
        //AudioListener

        // Calculate RMS (Root Mean Square) value of the audio samples
        float rms = 0f;
        foreach (float sample in samples)
        {
            //Debug.Log("audio:"+sample.ToString());
            rms += sample * sample;
        }
        rms = Mathf.Sqrt(rms / sampleSize);

        // Convert RMS to decibels (dB)
        audioLevel = rms > 0 ? 20f * Mathf.Log10(rms) : -74.8f; // -80 dB is silence
        audioLevel = Mathf.Clamp(audioLevel, -74.8f, 0f); // Clamp to a reasonable range

        currentValue = ((audioLevel + 74.8f) / 74.8f) * 100f;
        //Debug.Log($"Audio Level: {audioLevel:F2} dB" + n);
        BarSetter();
        //health.SetHealth(Mathf.RoundToInt(smoothedValue), gameObject);        
    }

    void BarSetter()
    {
        if (currentValue > smoothedValue)
        {
            // If the new value is greater, instantly update
            smoothedValue = currentValue;
        }
        else
        {
            // If the new value is less, lerp towards it
            smoothedValue = Mathf.Lerp(smoothedValue, currentValue, Time.deltaTime * lerpSpeed);
        }

        // For testing, log the values
        //Debug.Log($"Current: {currentValue}, Smoothed: {smoothedValue}");
    }
}

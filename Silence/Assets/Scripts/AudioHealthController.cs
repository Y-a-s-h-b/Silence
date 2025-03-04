using MoreMountains.CorgiEngine;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;


public class AudioHealthController : MonoBehaviour
{
    [HideInInspector]public float audioLevelAll; // Audio level in dB
    [HideInInspector]public float audioLevelMusic;        
    public float lerpSpeed = 0.05f;        
    private AudioSource musicAudioSource;
    public bool scriptEnabled;
    public bool dieable;
    public CinemachineCameraController cc;

    private CinemachineCamera camera;
    [HideInInspector] public float audioBar;
    private const int sampleAllSize = 1024; // Number of audio samples to analyze
    private const int sampleMusicSize = 1024;
    private float[] samplesAll;
    private float[] samplesMusic;
    public float smoothedValue;
    private float currentValueAll;
    private float currentValueMusic;
    private Health health;
    private float dieableStartBufferTime = 2f; // time after which a player can die. (after dieable is true)
        
    private float time;
    //public static float SmoothedValue;

    void Start()
    {
        samplesMusic = new float[sampleMusicSize];
        samplesAll = new float[sampleAllSize];
        smoothedValue = 0f;
        health = GetComponent<Health>();
        dieable = false;
        time = 0;
    }

    void Update()
    {

        OnVolumeNullTriggerDeath();
        if (!scriptEnabled) return;
        /*
        if (musicAudioSource == null)
        {
            musicAudioSource = GameObject.Find("MMAudioSourcePool_0").GetComponent<AudioSource>();
            musicAudioSource.volume = 0.052f;
        }*/

        if (dieable)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }
        BarSetter(GetDifferenceOfDecibles());
        audioBar = Mathf.RoundToInt(smoothedValue);
                                      

    }

    float GetDifferenceOfDecibles()
    {
        float[] spectrumData = new float[256]; // You can adjust the size based on accuracy and performance
        AudioListener.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris); // Use a suitable FFT window

        float rms = 0f;
        foreach (float sample in spectrumData)
        {
            rms += sample * sample;
        }

        rms = Mathf.Sqrt(rms / spectrumData.Length); // Calculate RMS from spectrum data
        float audioLevelAll = rms > 0 ? 20f * Mathf.Log10(rms) : -74.8f; // Convert RMS to dB
        audioLevelAll = Mathf.Clamp(audioLevelAll, -74.8f, 0f); // Clamp to a reasonable range
        float currentValueAll = ((audioLevelAll + 74.8f) / 74.8f) * 100f; // Normalize to 0-100 scale
        float val = Mathf.Max(currentValueAll, 0f); // Ensure no negative values

        return val;

        /*AudioListener.GetOutputData(samplesAll, 0);
        //musicAudioSource.GetOutputData(samplesMusic, 0);
        float rms = 0f;
        foreach (float sample in samplesAll)
        {
            rms += sample * sample;
        }
        rms = Mathf.Sqrt(rms / sampleAllSize);

        audioLevelAll = rms > 0 ? 20f * Mathf.Log10(rms) : -74.8f; // -80 dB is silence
        audioLevelAll = Mathf.Clamp(audioLevelAll, -74.8f, 0f); // Clamp to a reasonable range        
        currentValueAll = ((audioLevelAll + 74.8f) / 74.8f) * 100f;
        var val = Math.Max(currentValueAll, 0f);
        return val;*/

        /*
        float rmsN = 0f;
        foreach (float sample in samplesMusic)
        {
            rmsN += sample * sample;
        }
        rmsN = Mathf.Sqrt(rmsN / sampleMusicSize);

        audioLevelMusic = rmsN > 0 ? 20f * Mathf.Log10(rmsN) : -74.8f; // -80 dB is silence
        audioLevelMusic = Mathf.Clamp(audioLevelMusic, -74.8f, 0f); // Clamp to a reasonable range        
        currentValueMusic = ((audioLevelMusic + 74.8f) / 74.8f) * 100f;
        var val = Math.Max(currentValueAll - currentValueMusic, 0f);
            
        return val;*/
    }

    void BarSetter(float value)
    {
        if (value > smoothedValue)
        {
            // If the new value is greater, instantly update
            smoothedValue = value;
        }
        else
        {
            // If the new value is less, lerp towards it
            smoothedValue = Mathf.Lerp(smoothedValue, value, Time.deltaTime * lerpSpeed);
        }
        // For testing, log the values
        //Debug.Log($"Current: {currentValue}, Smoothed: {smoothedValue}");
    }

    private void LateUpdate()
    {
        if (cc == null)
        {
            camera = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineCamera;
            cc = camera.GetComponent<CinemachineCameraController>();
        }
        else
        {
            cc.PerformCustomOrthographicZoom(smoothedValue);
        }
    }

    private void OnVolumeNullTriggerDeath()
    {        
        if (smoothedValue <= 5 && dieable && time>= dieableStartBufferTime)
        {
            health.Kill();
            dieable = false;
            Debug.Log("disable dying");
            time = 0f;
        }
    }

}

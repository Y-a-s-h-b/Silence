using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using Unity.Cinemachine;
using Unity.Collections;
using UnityEngine;

public class AudioHealthController : MonoBehaviour
{
    [HideInInspector] public float audioLevelAll; // Audio level in dB
    [HideInInspector] public float audioLevelMusic;
    public float lerpSpeed = 0.5f;
    private AudioSource musicAudioSource;
    public bool scriptEnabled;
    public bool dieable;
    public CinemachineCameraController cc;
    private CinemachineCamera cam;
    private Health health;
    [HideInInspector] public float audioBar;
    private const int sampleAllSize = 1024; // Number of audio samples to analyze
    private const int sampleMusicSize = 1024;
    public float smoothedValue;
    //private Health health;
    [HideInInspector]
    public float dieableStartBufferTime = 2f; // time after which a player can die. (after dieable is true)
    [HideInInspector]
    public float time; 
    public List<AudioSource> _audioSources;
    public List<float> _samples;
    float bandValue;
    public OutputVolume outputVolume;
    void Start()
    {
        smoothedValue = 0f;
        health = GetComponent<Health>();
        dieable = false;
        time = 0;
        outputVolume = GetComponent<OutputVolume>();
    }

    void Update()
    {
        OnVolumeNullTriggerDeath();
        if (!scriptEnabled) return;

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
        float clampedValue = Mathf.Clamp(outputVolume.outputValue * 1000, 0, 80);
        return clampedValue;

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
            cam = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineCamera;
            cc = cam?.GetComponent<CinemachineCameraController>();
        }
        else
        {
            cc.PerformCustomOrthographicZoom(smoothedValue);
        }
    }

    private void OnVolumeNullTriggerDeath()
    {
        if (smoothedValue <= 5 && dieable && time >= dieableStartBufferTime)
        {
            health.Kill();
            dieable = false;
            Debug.Log("disable dying");
            time = 0f;
        }
    }
}
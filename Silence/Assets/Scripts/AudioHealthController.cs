using MoreMountains.CorgiEngine;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace test
{
    public class AudioHealthController : MonoBehaviour
    {
        [HideInInspector]public float audioLevelAll; // Audio level in dB
        [HideInInspector]public float audioLevelMusic;
        public float lerpSpeed = 0.05f;        
        public AudioSource audioSourceTest;
        public CinemachineCameraController cc;

        [HideInInspector] public float audioBar;
        public AudioMixer audioMixer;
        private AudioMixerGroup amg;
        private Health health;
        private const int sampleAllSize = 1024; // Number of audio samples to analyze
        private const int sampleMusicSize = 1024;
        private float[] samplesAll;
        private float[] samplesMusic;
        public static float smoothedValue;
        private float currentValueAll;
        private float currentValueMusic;
        //public static float SmoothedValue;

        void Start()
        {
            samplesMusic = new float[sampleMusicSize];
            samplesAll = new float[sampleAllSize];
            health = GetComponent<Health>();
            smoothedValue = 0f;
            
        }

        void Update()
        {

            BarSetter(GetDifferenceOfDecibles());
            audioBar = Mathf.RoundToInt(smoothedValue);

        }

        float GetDifferenceOfDecibles()
        {
            AudioListener.GetOutputData(samplesAll, 0);
            audioSourceTest.GetOutputData(samplesMusic, 0);
            //bool success = audioMixer.GetOutputData("asdf", samples, 0);

            // Calculate RMS (Root Mean Square) value of the audio samples
            float rms = 0f;
            foreach (float sample in samplesAll)
            {
                //Debug.Log("audio:"+sample.ToString());
                rms += sample * sample;
            }
            rms = Mathf.Sqrt(rms / sampleAllSize);

            audioLevelAll = rms > 0 ? 20f * Mathf.Log10(rms) : -74.8f; // -80 dB is silence
            audioLevelAll = Mathf.Clamp(audioLevelAll, -74.8f, 0f); // Clamp to a reasonable range        
            currentValueAll = ((audioLevelAll + 74.8f) / 74.8f) * 100f;

            float rmsN = 0f;
            foreach (float sample in samplesMusic)
            {
                //Debug.Log("audio:"+sample.ToString());
                rmsN += sample * sample;
            }
            rmsN = Mathf.Sqrt(rmsN / sampleMusicSize);

            audioLevelMusic = rmsN > 0 ? 20f * Mathf.Log10(rmsN) : -74.8f; // -80 dB is silence
            audioLevelMusic = Mathf.Clamp(audioLevelMusic, -74.8f, 0f); // Clamp to a reasonable range        
            currentValueMusic = ((audioLevelMusic + 74.8f) / 74.8f) * 100f;
            var val = Math.Max(currentValueAll - currentValueMusic, 0f);
            Debug.Log(smoothedValue);
            return val;
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
            cc.PerformCustomOrthographicZoom(smoothedValue);
        }
    }
}
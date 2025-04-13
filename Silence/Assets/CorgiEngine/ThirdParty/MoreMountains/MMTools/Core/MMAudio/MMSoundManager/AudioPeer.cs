using System.Runtime.InteropServices;
using UnityEngine;
namespace MoreMountains.Tools
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPeer : MonoBehaviour
    {
        const int BAND_8 = 8;
        AudioSource _audioSource;
        public static AudioBand AudioFrequencyBand8 { get; private set; }

        [DllImport("__Internal")]
        private static extern bool StartSampling(string name, float duration, int bufferSize);

        [DllImport("__Internal")] 
        private static extern bool CloseSampling(string name);

        [DllImport("__Internal")]
        private static extern bool GetSamples(string name, float[] freqData, int size);

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            AudioFrequencyBand8 = new AudioBand(BandCount.Eight);
            //if starting
            StartSampling("name", _audioSource.clip.length, 512);

        }
        public AudioSource GetSource()
        {
            return _audioSource;
        }
        public float GetTotalAudioBandValue()
        {
            float total = 0f;
            if (AudioFrequencyBand8 != null)
            {
                for (int i = 0; i < 8; i++) // Loop through all 8 bands
                {
                    total += AudioFrequencyBand8.GetAudioBand(i, true);
                }
            }
            return total;
        }
        public float GetAudioBandValue(int bandIndex)
        {
            if (AudioFrequencyBand8 != null)
            {
                return AudioFrequencyBand8.GetAudioBand(bandIndex,true);
            }
            return 0f; // Return 0 if the band doesn't exist
        }
            void Update()
            {
            if (_audioSource.isPlaying)
            {
                AudioFrequencyBand8.Update((sample) =>
                {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                    _audioSource.GetSpectrumData(sample, 0, FFTWindow.Blackman);
#endif
#if UNITY_WEBGL && !UNITY_EDITOR

                StartSampling(name, _audioSource.clip.length, 512);
                GetSamples(name, sample, sample.Length);
                Debug.Log(name +": " +GetAudioBandValue(0));

#endif
                });
            }
        }
    }
}
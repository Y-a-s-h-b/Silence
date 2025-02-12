using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using RengeGames.HealthBars;
using UnityEngine;
using test;

public class HealthBarUI : MonoBehaviour
{
    public RadialSegmentedHealthBar healthBar;
    public AudioHealthController audioHealthController;
    public float maxVolume = 50f; //original is 75;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GetComponent<RadialSegmentedHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.Instance != null && LevelManager.Instance.Players[0] && audioHealthController == null)
        {
            audioHealthController = LevelManager.Instance.Players[0].GetComponent<AudioHealthController>();
        }
        healthBar.RemoveSegments.Value = healthBar.SegmentCount.Value - ((audioHealthController.audioBar * healthBar.SegmentCount.Value)/ maxVolume);
    }
}

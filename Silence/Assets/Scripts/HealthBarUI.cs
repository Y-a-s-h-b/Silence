using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using RengeGames.HealthBars;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    public RadialSegmentedHealthBar healthBar;
    public LevelManager levelManager;
    public AudioTesting audioTesting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GetComponent<RadialSegmentedHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(levelManager == null) levelManager = LevelManager.Instance;
        else
        {
            audioTesting = levelManager.Players[0].GetComponent<AudioTesting>();
        }
        //Debug.Log(progressBar.BarProgress);
        healthBar.RemoveSegments.Value = healthBar.SegmentCount.Value - ((audioTesting.audioBar * healthBar.SegmentCount.Value)/85);
    }
}

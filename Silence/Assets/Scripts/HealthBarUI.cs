using MoreMountains.Tools;
using RengeGames.HealthBars;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    public RadialSegmentedHealthBar healthBar;
    public MMProgressBar progressBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GetComponent<RadialSegmentedHealthBar>();
        progressBar = GetComponentInParent<MMProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(progressBar.BarProgress);
        healthBar.RemoveSegments.Value = healthBar.SegmentCount.Value - (progressBar.BarProgress * healthBar.SegmentCount.Value);
    }
}

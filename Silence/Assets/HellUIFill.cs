using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using RengeGames.HealthBars;
using UnityEngine;
using test;

public class HellUIFill : MonoBehaviour
{
    public RadialSegmentedHealthBar fillBar;
    private void Start()
    {
        ResetFill();
    }
    // Update is called once per frame
    void Update()
    {
        if(QTEManager.Instance != null && QTEManager.Instance.holding)
        {
            if(fillBar.RemoveSegments.Value < 0)
            {
                fillBar.RemoveSegments.Value = 1;
            }
            fillBar.RemoveSegments.Value = 1 - QTEManager.Instance.holdTime/2;
        }
    }
    public void ResetFill()
    {
        fillBar.RemoveSegments.Value = 1;
    }
}

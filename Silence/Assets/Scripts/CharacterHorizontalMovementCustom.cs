using MoreMountains.CorgiEngine;
using UnityEngine;

public class CharacterHorizontalMovementCustom : CharacterHorizontalMovement
{
    private float timeAtEdgeValue = 0f;
    public float runThresholdTime = 2f;
    private bool runFunOnce = true;

    
    private void Update()
    {
        if(_horizontalInput ==1 || _horizontalInput == -1)
        {
            timeAtEdgeValue += Time.deltaTime;
            if (timeAtEdgeValue >= runThresholdTime && runFunOnce)
            {
                //changeSpeed
                IncreaseSpeed();
                runFunOnce = false;
            }
        }
        else
        {
            timeAtEdgeValue = 0f;
            MovementSpeed = WalkSpeed;
            runFunOnce = true;
        }
    }
    void IncreaseSpeed()
    {
        MovementSpeed = 16f;
    }
}

using MoreMountains.CorgiEngine;
using System;
using System.Collections;
using UnityEngine;

public class MovementSoundHandler : MonoBehaviour
{
    CharacterHorizontalMovement charMove;    
    Vector2 oldPosition;
    Vector2 newPosition;
    public float updaterTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charMove = GetComponent<CharacterHorizontalMovement>();
        oldPosition = transform.position;
        StartCoroutine(CalculateDistance());        
    }
    
    IEnumerator CalculateDistance()
    {        
        while (true)
        {
            newPosition = transform.position;
            UpdateSoundBar(newPosition, oldPosition);
            yield return new WaitForSeconds(updaterTime);
            oldPosition = newPosition;
        }
    }

    void UpdateSoundBar(Vector2 oldPosition, Vector3 newPosition)
    {
        float disVal = Vector3.Distance(oldPosition, newPosition);        
        Debug.Log(Math.Round(disVal,2)*5f);
    }
}

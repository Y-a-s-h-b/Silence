using MoreMountains.CorgiEngine;
using System.Collections;
using UnityEngine;

public class MovementSoundHandler : MonoBehaviour
{
    CharacterHorizontalMovement charMove;
    public Health charHealth;
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
        //Debug.Log(charHealth.CurrentHealth);
        //charHealth.CurrentHealth = disVal+1;
        charHealth.CurrentHealth = disVal+1;
    }
}

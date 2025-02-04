using System.Net;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class StartSequence : MonoBehaviour
{
    public Character character;
    public GameObject cameraTarget;
    public float duration = 2f;
    private float elapsedTime = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (character == null) character = LevelManager.Instance?.Players[0].GetComponent<Character>();
        if (character != null) 
        {
            cameraTarget = character.CameraTarget;
            if (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                transform.position = Vector3.Lerp(cameraTarget.transform.position, new Vector3(0,0,0), t);
            }
        }
    }
}

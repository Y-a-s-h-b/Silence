using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public static QTEManager Instance;
    public bool holding = false;
    public float holdTime;

    public int clickCount;
    public float clickElapsedTime;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public async Task<bool> RapidClickAsync(float tappingDuration, int requiredClicks, KeyCode interactionKey)
    {
        clickElapsedTime = 0f;
        clickCount = 0;

        while (clickElapsedTime < tappingDuration)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                clickCount++;
                Debug.Log($"Pressed E: {clickCount}/{requiredClicks}");

                if (clickCount >= requiredClicks)
                {
                    return true;
                }
            }

            clickElapsedTime += Time.deltaTime;
            await Task.Yield();
        }

        return false;
    }

    public async Task<bool> HoldAsync(float holdDuration, KeyCode interactionKey)
    {
        float elapsedTime = 0f;
        holdTime = elapsedTime;
        while (Input.GetKey(interactionKey))
        {
            holding = true;
            elapsedTime += Time.deltaTime;
            holdTime = elapsedTime;
            Debug.Log($"Holding: {elapsedTime}/{holdDuration}");

            if (elapsedTime >= holdDuration)
            {
                holding = false;
                return true;
            }

            await Task.Yield();
        }
        holding = false;
        return false;
    }
}

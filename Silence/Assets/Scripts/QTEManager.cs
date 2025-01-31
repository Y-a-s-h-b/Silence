using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public static QTEManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public async Task<bool> RapidClickAsync(float tappingDuration, int requiredClicks, KeyCode interactionKey)
    {
        float elapsedTime = 0f;
        int clickCount = 0;

        while (elapsedTime < tappingDuration)
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

            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }

        return false;
    }

    public async Task<bool> HoldAsync(float holdDuration, KeyCode interactionKey)
    {
        float elapsedTime = 0f;

        while (Input.GetKey(interactionKey))
        {
            elapsedTime += Time.deltaTime;

            Debug.Log($"Holding: {elapsedTime}/{holdDuration}");

            if (elapsedTime >= holdDuration)
            {
                return true;
            }

            await Task.Yield();
        }

        return false;
    }
}

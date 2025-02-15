using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class GUIDisplay : MonoBehaviour
{
    [SerializeField] private MMRadialProgressBar[] displayUI;
    private bool isActive = false;

    public void Activate()
    {
        if (isActive)
        {
            return;
        }

        isActive = true;
        foreach (var display in displayUI)
        {
            display.gameObject.SetActive(true);
        }
    }

    public void UpdateUI(float currentValue, float minValue, float maxValue, int displayToUpdate = 0)
    {
        displayUI[displayToUpdate].UpdateBar(currentValue, minValue, maxValue);
    }

    public void Deactivate()
    {
        isActive = false;
        foreach (var display in displayUI)
        {
            display.gameObject.SetActive(false);
        }
    }
}

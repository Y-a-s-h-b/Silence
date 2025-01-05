using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider slider; // Reference to the slider
    public TextMeshProUGUI valueText; // Reference to the text element
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (slider != null)
        {
            // Initialize the text with the slider's current value
            UpdateSliderText(slider.value);

            // Add a listener to the slider to update the text when the value changes
            slider.onValueChanged.AddListener(UpdateSliderText);
        }
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    // Method to update the text
    private void UpdateSliderText(float value)
    {
        int percentage = Mathf.RoundToInt(value * 100); // Convert to whole percentage
        valueText.text = percentage + "%";
    }

    private void OnDestroy()
    {
        if (slider != null)
            // Remove listener to avoid memory leaks
            slider.onValueChanged.RemoveListener(UpdateSliderText);
    }
}

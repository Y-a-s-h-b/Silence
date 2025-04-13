using System.Collections;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

public class FadeonStart : MonoBehaviour
{
    public Image image; // Assign in Inspector
    public float fadeDuration = 1.5f; // Time to fade out
    private void Start()
    {
        if (image == null) image = GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1);
        Color color = image.color;
        float startAlpha = color.a;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            image.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        image.color = new Color(color.r, color.g, color.b, 0f); // Ensure it's fully transparent
    }
}

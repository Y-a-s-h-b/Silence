using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CrystalHandler : MonoBehaviour
{
    public Image[] shards; // Assign the 4 shard UI images
    private int currentShardIndex = 0;

    public float fadeDuration = 0.5f;
    public float moveDuration = 0.5f;
    public Vector2 startOffset = new Vector2(50, 50);

    public void CollectShard()
    {
        if (currentShardIndex >= shards.Length)
            return; // Already full

        Image shard = shards[currentShardIndex];
        shard.gameObject.SetActive(true);
        StartCoroutine(AnimateShard(shard));

        currentShardIndex++;
    }

    private IEnumerator AnimateShard(Image shard)
    {
        RectTransform rect = shard.GetComponent<RectTransform>();
        CanvasGroup canvasGroup = shard.GetComponent<CanvasGroup>();

        // Start position
        Vector3 originalPos = rect.anchoredPosition;
        rect.anchoredPosition += startOffset;

        // Fade and move in
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, originalPos, t);
            canvasGroup.alpha = t;
            yield return null;
        }

        // Ensure it's exactly at final position
        rect.anchoredPosition = originalPos;
        canvasGroup.alpha = 1;
    }
}

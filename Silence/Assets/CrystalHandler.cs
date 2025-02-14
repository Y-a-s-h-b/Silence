using System.Collections;
using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CrystalHandler : MMSingleton<CrystalHandler>
{
    public Image[] shards; // Assign the 4 shard UI images
    private int currentShardIndex = 0;

    public float fadeDuration = 0.5f;
    public float moveDuration = 0.5f;
    public Vector2 startOffset = new Vector2(50, 50);
    public MMF_Player Shardfeedback;
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
        GameManager.Instance.Pause(PauseMethods.NoPauseMenu);
        RectTransform rect = shard.GetComponent<RectTransform>();
        CanvasGroup canvasGroup = shard.GetComponent<CanvasGroup>();

        // Start position
        Vector3 originalPos = new Vector3(-12, 0, 0);
        rect.anchoredPosition += startOffset;

        // Fade and move in
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = elapsedTime / fadeDuration;
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, originalPos, t);
            canvasGroup.alpha = t;
            yield return null;
        }

        // Ensure it's exactly at final position
        rect.anchoredPosition = originalPos;
        canvasGroup.alpha = 1;
        Shardfeedback.PlayFeedbacks();
        GameManager.Instance.UnPause(PauseMethods.NoPauseMenu);
        yield return new WaitForSeconds(.5f);
        SetAlpha();
    }
    private void SetAlpha()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }
}

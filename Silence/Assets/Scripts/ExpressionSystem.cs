using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Expressions
{
    public string name;  // Unique name for the expression
    public Sprite sprite;
    public float duration;
}

public class ExpressionSystem : MonoBehaviour
{
    public Image expressionImage; // UI Image to display expressions
    public List<Expressions> expressionList = new List<Expressions>(); // List of predefined expressions
    private Queue<Expressions> expressionQueue = new Queue<Expressions>();
    private Dictionary<string, Expressions> expressionDict = new Dictionary<string, Expressions>();

    private bool isPlaying = false;

    private void Awake()
    {
        // Populate dictionary for quick lookup
        foreach (var expression in expressionList)
        {
            if (!expressionDict.ContainsKey(expression.name))
            {
                expressionDict.Add(expression.name, expression);
            }
            else
            {
                Debug.LogWarning($"Duplicate expression name found: {expression.name}");
            }
        }
    }

    public void ShowExpression(string expressionName)
    {
        if (expressionDict.TryGetValue(expressionName, out Expressions expression))
        {
            ShowExpression(expression.sprite, expression.duration);
        }
        else
        {
            Debug.LogWarning($"Expression '{expressionName}' not found!");
        }
    }

    private void ShowExpression(Sprite sprite, float duration)
    {
        Expressions newExpression = new Expressions { sprite = sprite, duration = duration };
        expressionQueue.Enqueue(newExpression);

        if (!isPlaying)
        {
            StartCoroutine(PlayExpressions());
        }
    }

    private IEnumerator PlayExpressions()
    {
        isPlaying = true;
        while (expressionQueue.Count > 0)
        {
            Expressions current = expressionQueue.Dequeue();
            expressionImage.sprite = current.sprite;
            expressionImage.enabled = true;
            yield return new WaitForSeconds(current.duration);
            expressionImage.enabled = false;
        }
        isPlaying = false;
    }
}

using MoreMountains.CorgiEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CoinDropHandler : MonoBehaviour
{
    private CorgiController controller;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float positionUpdateDuration = 1.5f;
    [SerializeField] private Vector2 dropRange = new (4, 1);
    private Vector3 safePoint;

    private void Start()
    {
        controller = GetComponent<CorgiController>();
        StartCoroutine(UpdateCoinPosition());
    }

    public void DropRandomCoins(int amount)
    {
        int range = Random.Range(amount/3, amount/2);
        StartCoroutine(StartDropCoins(range));
    }

    private IEnumerator StartDropCoins(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 dropPosition = new(Random.Range(safePoint.x - dropRange.x, safePoint.x + dropRange.x),
                                       Random.Range(safePoint.y, safePoint.y + dropRange.y),
                                       0);

            yield return new WaitForSeconds(0.1f);
            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }
    }

    private IEnumerator UpdateCoinPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(positionUpdateDuration);

            while (!controller.State.IsGrounded && Mathf.Approximately(controller.Speed.y, 0))
            {
                yield return null;
            }

            if (controller.State.IsGrounded && controller.transform.position.y > -60)
            {
                safePoint = controller.transform.position;
            }
        }
    }
}
using MoreMountains.CorgiEngine;
using Unity.VisualScripting;
using UnityEngine;

public class Sacrifice : MonoBehaviour
{
    [SerializeField] private float holdDuration = 2f;
    [SerializeField] private KeyCode interactionKey = KeyCode.Space;
    [SerializeField] private bool isActive = false;

    private GameObject player;
    private bool isSuccess = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        player = collision.gameObject;

        isActive = true;
    }

    private void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(interactionKey))
        {
            StartSacrifice();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isActive = false;
    }

    public async void StartSacrifice()
    {
        isSuccess = await QTEManager.Instance.HoldAsync(holdDuration, interactionKey);

        if (isSuccess)
        {
            SacrificeCompleted();
        }
    }

    private void SacrificeCompleted()
    {
        GameManager.Instance.LoseLife();
        LevelManager.Instance.PlayerDead(player.GetComponent<Character>());
        if(OnTriggerPlayFeedback.Instance) OnTriggerPlayFeedback.Instance.m_IsPlaying = false;
    }
}

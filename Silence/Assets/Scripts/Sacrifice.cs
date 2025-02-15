using Febucci.UI.Effects;
using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using Unity.VisualScripting;
using UnityEngine;

public class Sacrifice : MonoBehaviour, MMEventListener<CorgiEngineEvent>
{
    [SerializeField] private float holdDuration = 2f;
    [SerializeField] private KeyCode interactionKey = KeyCode.Space;
    [SerializeField] private bool isActive = false;
    [HideInInspector]
    public GameObject player;
    private bool isSuccess = false;
    public MMF_Player FadeOutFeedback;
    public MMF_Player FadeInFeedback;
    public Material DefaultMat;

    private void OnEnable() => this.MMEventStartListening<CorgiEngineEvent>();
    private void OnDisable() => this.MMEventStopListening<CorgiEngineEvent>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        player = collision.gameObject;

        isActive = true;
    } private void OnTriggerStay2D(Collider2D collision)
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
            FadeInFeedback.PlayFeedbacks();
            if (OnTriggerPlayFeedback.Instance) OnTriggerPlayFeedback.Instance.m_IsPlaying = false;
            SacrificeCompleted();
        }
    }

    private void SacrificeCompleted()
    {
        //enable dieable
        Debug.Log("enable dying");
        player.GetComponent<AudioHealthController>().dieable = true;
        GameManager.Instance.LoseLife();
        LevelManager.Instance.PlayerDead(player.GetComponent<Character>());
        if(!GUIManager.Instance.HUD.activeSelf) GUIManager.Instance.SetHUDActive(true);
        player.GetComponent<Character>().CharacterModel.GetComponent<SpriteRenderer>().material = DefaultMat;
    }

    public void OnMMEvent(CorgiEngineEvent eventType)
    {
        if (eventType.EventType == CorgiEngineEventTypes.Respawn)
        {
            FadeOutFeedback.PlayFeedbacks();
            if (OnTriggerPlayFeedback.Instance) OnTriggerPlayFeedback.Instance.m_IsPlaying = false;
        }
    }
}

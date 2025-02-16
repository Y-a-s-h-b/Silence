using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class BossEnemyHealthModifier : MonoBehaviour, MMEventListener<HealthDeathEvent>, MMEventListener<CorgiEngineEvent>
{
    private Health health;
    public GameObject[] bossEnemySpeakers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        this.MMEventStartListening<HealthDeathEvent>();
        this.MMEventStartListening<CorgiEngineEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<HealthDeathEvent>();
        this.MMEventStopListening<CorgiEngineEvent>();
    }

    public void OnMMEvent(HealthDeathEvent e)
    {
        if (e.AffectedHealth.transform.CompareTag("EnemyBreakableSpeaker"))
        {
            health.Damage(300, this.gameObject, 0.2f, 0.2f, Vector3.up);
            TurnSpeakerMode(e.AffectedHealth.transform.gameObject, false);
        }        
    }

    public void TurnSpeakerMode(GameObject speaker, bool flag)
    {
        /*
        speaker.GetComponent<SpriteRenderer>().enabled = flag;
        speaker.GetComponent<BoxCollider2D>().enabled = flag;
        speaker.GetComponent<AudioSource>().enabled = flag;
        */        

        if (flag)
        {
            speaker.MMGetOrAddComponent<Health>().ResetHealthToMaxHealth();

        }
        else
        {
            speaker.GetComponent<AudioSource>().Stop();
        }
        speaker.SetActive(flag);
       
    }

    public void OnMMEvent(CorgiEngineEvent eventType)
    {
        if (eventType.EventType == CorgiEngineEventTypes.PlayerDeath)
        {
            var player = LevelManager.Instance.Players[0].gameObject;
            foreach (var speaker in bossEnemySpeakers)
            {
                TurnSpeakerMode(speaker, false);
                Debug.Log("death at fram" + Time.frameCount);
            }

        }
        if (eventType.EventType == CorgiEngineEventTypes.Respawn)
        {
            var player = LevelManager.Instance.Players[0].gameObject;
            foreach (var speaker in bossEnemySpeakers)
            {
                
                TurnSpeakerMode(speaker, true);
                Debug.Log("respawn at fram" + Time.frameCount);
            }

        }

    }


}

using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class BossEnemyHealthModifier : MonoBehaviour, MMEventListener<HealthDeathEvent>
{
    public Health health;

    public GameObject[] bossEnemySpeakers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        this.MMEventStartListening<HealthDeathEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<HealthDeathEvent>();
    }

    public void OnMMEvent(HealthDeathEvent e)
    {
        if (e.AffectedHealth.transform.CompareTag("EnemyBreakableSpeaker"))
        {
            health.Damage(25, this.gameObject, 0.2f, 0.2f, Vector3.up);
        }        
    }
}

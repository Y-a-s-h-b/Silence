using MoreMountains.CorgiEngine;
using UnityEngine;

public class AfterEnemyDeathGateOpen : MonoBehaviour
{
    private GameObject enemy;
    private void Start()
    {
        enemy = GameObject.Find("BossEnemy");
    }

    private void Update()
    {
        if (enemy.GetComponent<Health>().CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

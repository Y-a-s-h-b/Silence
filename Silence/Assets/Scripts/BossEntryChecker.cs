using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class BossEntryChecker : MonoBehaviour, MMEventListener<CorgiEngineEvent>
{
    private CinemachineCameraController cc;
    private CinemachineCamera cam;
    private AudioHealthController ahc;
    private GameObject bossEnemy;
    public GameObject[] walls;
    public GameObject camTarget;

    private void OnEnable()
    {
        this.MMEventStartListening<CorgiEngineEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<CorgiEngineEvent>();
    }
    private void Start()
    {
        if (cc == null)
        {
            cam = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineCamera;
            cc = cam.GetComponent<CinemachineCameraController>();
        }

        bossEnemy = GameObject.Find("BossEnemy");

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.gameObject;
        if (player.CompareTag("Player"))
        {
            //Debug.Log("in");
            if(ahc==null) ahc = player.GetComponent<AudioHealthController>();
            player.GetComponent<Collider2D>().isTrigger = false;
            ahc.smoothedValue = 60f;
            ahc.scriptEnabled = false;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject;
        if (player.CompareTag("Player"))
        {
            bossEnemy.GetComponent<AIBrain>().enabled = true;
            // ChangeLayerOfWall(true);
            cam.Follow = camTarget.transform;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject;
        if (player.CompareTag("Player"))
        {
            if (!ahc) ahc = player.GetComponent<AudioHealthController>();
            player.GetComponent<Collider2D>().isTrigger = true;
            ahc.scriptEnabled = true;
            ahc.dieable = true;
            bossEnemy.GetComponent<AIBrain>().enabled = false;
            cam.Follow = player.transform;
        }
    }

    private void ChangeLayerOfWall(bool flag) // if true -> player cannot movefrom wall
    {
        if (flag)
        {
            foreach (var wall in walls)
            {
                wall.layer = LayerMask.NameToLayer("Platforms");
            }
        }
        else
        {
            foreach (var wall in walls)
            {
                wall.layer = LayerMask.NameToLayer("EnemyPlatform");
            }
        }
    }

    public void OnMMEvent(CorgiEngineEvent eventType)
    {
        
        if (eventType.EventType == CorgiEngineEventTypes.PlayerDeath)
        {
            Debug.Log("death");
        }
    }
}

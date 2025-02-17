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
    public GameObject camTarget;
    public GameObject playerEntryGate;
    public GameObject playerExitGate;
    

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
            cam.Lens.OrthographicSize = 5.6f;

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
            cam.GetComponent<CinemachineFollow>().FollowOffset = new Vector3(-0.4f, 1, -10f);
            cam.Lens.OrthographicSize = 5.6f;
            CloseBossEntryExitGate(true);
            bossEnemy.GetComponent<Health>().ResetHealthToMaxHealth();
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
            cam.Lens.OrthographicSize = 8.39f;
            cam.GetComponent<CinemachineFollow>().FollowOffset = new Vector3(-0f, 2, -10f);
            bossEnemy.GetComponent<Health>().ResetHealthToMaxHealth();
            CloseBossEntryExitGate(false);

        }
    }

   

    public void OnMMEvent(CorgiEngineEvent eventType)
    {
        
        if (eventType.EventType == CorgiEngineEventTypes.PlayerDeath)
        {
            Debug.Log("death");
        }
    }

    private void CloseBossEntryExitGate(bool flag) //if true it will close
    {
        playerEntryGate.GetComponent<Collider2D>().enabled = flag;
        playerExitGate.GetComponent<Collider2D>().enabled = flag;
    }
}

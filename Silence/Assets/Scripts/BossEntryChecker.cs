using MoreMountains.CorgiEngine;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class BossEntryChecker : MonoBehaviour
{
    private CinemachineCameraController cc;
    private CinemachineCamera cam;
    private AudioHealthController ahc;
    private void Start()
    {
        if (cc == null)
        {
            cam = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineCamera;
            cc = cam.GetComponent<CinemachineCameraController>();
        }
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject;
        if (player.CompareTag("Player"))
        {
            if (!ahc) ahc = player.GetComponent<AudioHealthController>();
            player.GetComponent<Collider2D>().isTrigger = false;
            ahc.scriptEnabled = true;

        }
    }




}

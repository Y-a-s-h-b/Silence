using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using Unity.Cinemachine;
using UnityEngine;

public class CameraHealthController : MonoBehaviour
{
    private AudioHealthController AudioHealthController;
    public CinemachineCameraController cc;
    private CinemachineCamera cam;
    private Health health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioHealthController = GetComponent<AudioHealthController>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        OnVolumeNullTriggerDeath();
        if (!AudioHealthController.scriptEnabled) return;
    }
    private void LateUpdate()
    {
        if (cc == null)
        {
            cam = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineCamera;
            cc = cam?.GetComponent<CinemachineCameraController>();
        }
        else
        {
            cc.PerformCustomOrthographicZoom(AudioHealthController.smoothedValue);
        }
    }
    private void OnVolumeNullTriggerDeath()
    {
        if (AudioHealthController.smoothedValue <= 1 && AudioHealthController.dieable && AudioHealthController.time >= AudioHealthController.dieableStartBufferTime)
        {
            health.Kill();
            AudioHealthController.dieable = false;
            Debug.Log("disable dying");
            AudioHealthController.time = 0f;
        }
    }
}

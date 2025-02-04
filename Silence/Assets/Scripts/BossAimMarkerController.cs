using MoreMountains.CorgiEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossAimMarkerController : MonoBehaviour
{
    public Image backgroundImage;
    public Image forgroundImage;
    [Tooltip("Float value - Aim to be stopped at this duration from fire")]
    public float weaponAimStopBeforeShoot = 0.5f;

    private Weapon weapon;
    private WeaponAim weaponAim;
    private float delayBeforeWeaponUse;
    private float lerpDuration= 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundImage.enabled = true;
        forgroundImage.enabled = true;
        weapon = transform.GetComponentInParent<Weapon>();
        weaponAim = transform.GetComponentInParent<WeaponAim>();
        if (weapon)
        {
            delayBeforeWeaponUse = weapon.DelayBeforeUse;
        }
        Debug.Log("delay time" + delayBeforeWeaponUse);
        StartCoroutine(ChangeValueOverTime(delayBeforeWeaponUse - weaponAimStopBeforeShoot));
        StartCoroutine(StopAimMarker(delayBeforeWeaponUse+1f));
    }
    IEnumerator StopAimMarker(float time)
    {
        yield return new WaitForSeconds(time);
        backgroundImage.enabled = false;
        forgroundImage.enabled = false;
    }
    IEnumerator ChangeValueOverTime(float time)
    {
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            lerpDuration = Mathf.Lerp(0f, 1f, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            forgroundImage.fillAmount = lerpDuration;
            yield return null;
        }
        lerpDuration = 1f; // Ensure it reaches exactly 1 at the end

        forgroundImage.fillAmount = lerpDuration;
        weaponAim.WeaponRotationSpeed = 0.001f;
    }
}

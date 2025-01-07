using MoreMountains.CorgiEngine;
using UnityEngine;

public class AbilityEnabler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string ablilityName;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterAbility var = (CharacterAbility)collision.gameObject.GetComponent(ablilityName);
            var.AbilityPermitted = true;
            Destroy(gameObject);
        }
    }
}

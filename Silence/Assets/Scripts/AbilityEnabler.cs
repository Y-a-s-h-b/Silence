using MoreMountains.CorgiEngine;
using System.Collections;
using UnityEngine;

public class AbilityEnabler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string ablilityName;

/*    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("colliderd");
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterAbility var = (CharacterAbility)collision.gameObject.GetComponent(ablilityName);
            var.AbilityPermitted = true;
            StartCoroutine(FreezePlayerFor(timeToFreeze, collision.gameObject));
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }*/


   
}

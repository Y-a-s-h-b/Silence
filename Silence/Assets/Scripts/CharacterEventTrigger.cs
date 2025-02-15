using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using Unity.Cinemachine;
using UnityEngine;

public class CharacterEventTrigger : MonoBehaviour
{
    public Character _currentCharacter;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _currentCharacter = collision.gameObject.MMGetComponentNoAlloc<Character>();
            _currentCharacter.Reset();
            _currentCharacter.Freeze();
            _currentCharacter.GetComponent<CharacterHorizontalMovement>().PermitAbility(false);
            //_currentCharacter.enabled = false;
            //InputManager.Instance.InputDetectionActive = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            //_currentCharacter.Freeze();
            //_currentCharacter.enabled = false;
           
    }
}

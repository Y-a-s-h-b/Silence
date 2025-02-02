using MoreMountains.CorgiEngine;
using System.Collections;
using UnityEngine;

public class Vines : MonoBehaviour
{
    [SerializeField] private int requiredClicks = 10;
    [SerializeField] private float tappingDuration = 5f;
    [SerializeField] private float speedMultiplier = 0.1f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    private Character trappedCharacter;
    private CharacterHorizontalMovement characterHorizontalMovement;
    private float originalSpeedMultiplier;
    private float abilitySpeedMultiplier;

    private bool isTrapped = false;

    private void Start()
    {
        trappedCharacter = LevelManager.Instance.SceneCharacters[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        if (!collision.gameObject.TryGetComponent(out Character character) || isTrapped)
        {
            return;
        }

        isTrapped = true;
        trappedCharacter = character;

        Debug.Log("Trapped!");

        RestrictCharacter();
        StartRapidClicks();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrapped = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private async void StartRapidClicks()
    {
        bool isSuccess = await QTEManager.Instance.RapidClickAsync(tappingDuration, requiredClicks, interactionKey);

        if (isSuccess)
        {
            FreeCharacter();
        }
        else
        {
            ResetCharacter();
        }
    }

    private void RestrictCharacter()
    {
        //Other Abilities
        trappedCharacter.GetComponent<CharacterJump>().AbilityPermitted = false;
        trappedCharacter.GetComponent<CharacterDashBreakable>().AbilityPermitted = false;
        
        //Speed Multiplier
        characterHorizontalMovement = trappedCharacter.GetComponent<CharacterHorizontalMovement>();
        originalSpeedMultiplier = characterHorizontalMovement.MovementSpeedMultiplier;
        abilitySpeedMultiplier = characterHorizontalMovement.AbilityMovementSpeedMultiplier;
        characterHorizontalMovement.MovementSpeedMultiplier = speedMultiplier;
        characterHorizontalMovement.AbilityMovementSpeedMultiplier = speedMultiplier;
    }

    private void FreeCharacter()
    {
        Debug.Log("Player freed!");
        GetComponent<SpriteRenderer>().color = Color.white;
        ResetCharacter();
    }

    private void ResetCharacter()
    {
        //Speed Multipler
        characterHorizontalMovement.MovementSpeedMultiplier = originalSpeedMultiplier;
        characterHorizontalMovement.AbilityMovementSpeedMultiplier = abilitySpeedMultiplier;

        //Other Ability 
        trappedCharacter.GetComponent<CharacterJump>().AbilityPermitted = true;
        trappedCharacter.GetComponent<CharacterDashBreakable>().AbilityPermitted = true;
    }

    #region Reworked

    //private IEnumerator RapidClickCoroutine()
    //{
    //    float elapsedTime = 0f;

    //    while (elapsedTime < tappingDuration)
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            clickCount++;
    //            Debug.Log($"Pressed E: {clickCount}/{requiredClicks}");

    //            if (clickCount >= requiredClicks)
    //            {
    //                FreeCharacter();
    //                yield break;
    //            }
    //        }

    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    Debug.Log("FAiled");
    //    trappedCharacter.CharacterHealth.Kill();
    //    ResetCharacter();
    //}

    #endregion
}

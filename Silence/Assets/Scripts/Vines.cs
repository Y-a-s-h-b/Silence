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
    private GUIDisplay display;
    private QTEManager qteManager;
    private float originalSpeedMultiplier;
    private float abilitySpeedMultiplier;

    private bool isTrapped = false;

    private void Awake()
    {
        display = GetComponentInParent<GUIDisplay>();
        display.Deactivate();
    }

    private void Start()
    {
        trappedCharacter = LevelManager.Instance?.Players[0];
        qteManager = QTEManager.Instance;
    }

    private void Update()
    {

        if (isTrapped)
        {
            display.UpdateUI(requiredClicks - qteManager.clickCount, 0, requiredClicks, 0);
            display.UpdateUI(tappingDuration - qteManager.clickElapsedTime, 0, tappingDuration, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trappedCharacter = LevelManager.Instance.Players[0];
        GetComponent<SpriteRenderer>().color = Color.red;
        if (!collision.gameObject.TryGetComponent(out Character character) || isTrapped)
        {
            return;
        }

        if (character.MovementState.CurrentState == CharacterStates.MovementStates.Dashing)
        {
            return;
        }


        isTrapped = true;
        trappedCharacter = character;

        Debug.Log("Trapped!");

        RestrictCharacter();
        StartRapidClicks();
        display.Activate();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrapped = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private async void StartRapidClicks()
    {
        if (qteManager == null)
        {
            qteManager = QTEManager.Instance;
        }

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

        //Update Display
        display.Deactivate();
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

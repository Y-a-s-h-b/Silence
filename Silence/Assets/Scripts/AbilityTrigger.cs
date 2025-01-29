using MoreMountains.CorgiEngine;
using UnityEngine;

public class AbilityTrigger : MonoBehaviour
{
    public Abilities[] AbilitiesToToggle;

    public bool SlowPlayer;
    public float SlowSpeedMultiplier = 0.5f;

    public bool StopPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var ability in AbilitiesToToggle)
            {
                AbilityManager.Instance.DisableAbility(ability);
            }

            if (SlowPlayer)
            {
                var movementController = other.gameObject.GetComponent<CharacterHorizontalMovement>();
                movementController.MovementSpeedMultiplier = SlowSpeedMultiplier;
                //movementController.AbilityMovementSpeedMultiplier = SlowSpeedMultiplier;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var ability in AbilitiesToToggle)
            {
                AbilityManager.Instance.EnableAbility(ability);
            }

            if (SlowPlayer)
            {
                var movementController = other.gameObject.GetComponent<CharacterHorizontalMovement>();
                movementController.MovementSpeedMultiplier = 1f;
                //movementController.AbilityMovementSpeedMultiplier = 1f;
            }
        }
    }
}

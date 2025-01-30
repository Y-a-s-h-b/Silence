using MoreMountains.CorgiEngine;
using UnityEngine;

public enum Abilities { Jump, Sprint, Dash }

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
                DisableAbility(ability, other.gameObject);
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
                EnableAbility(ability, other.gameObject);
            }

            if (SlowPlayer)
            {
                var movementController = other.gameObject.GetComponent<CharacterHorizontalMovement>();
                movementController.MovementSpeedMultiplier = 1f;
                //movementController.AbilityMovementSpeedMultiplier = 1f;
            }
        }
    }


    private void EnableAbility(Abilities ability, GameObject player)
    {
        if (ability.Equals(Abilities.Sprint))
        {
            SetSprintAbility(player, true);
        }

        else if (ability.Equals(Abilities.Jump))
        {
            SetJumpAbility(player, true);
        }

        else if (ability.Equals(Abilities.Dash))
        {
            SetDashAbility(player, true);
        }
    }

    private void DisableAbility(Abilities ability, GameObject player)
    {
        if (ability.Equals(Abilities.Sprint))
        {
            SetSprintAbility(player, false);
        }

        else if (ability.Equals(Abilities.Jump))
        {
            SetJumpAbility(player, false);
        }

        else if (ability.Equals(Abilities.Dash))
        {
            SetDashAbility(player, false);
        }
    }

    private void SetSprintAbility(GameObject player, bool enable)
    {
        var sprintController = player.GetComponent<CharacterSprint>();
        var runController = player.GetComponent<CharacterRun>();
        sprintController.AbilityPermitted = enable;
        sprintController.RunStop();
        runController.AbilityPermitted = enable;
        runController.RunStop();
    }

    private void SetJumpAbility(GameObject player, bool enable)
    {
        player.GetComponent<CharacterJump>().AbilityPermitted = enable;
    }

    private void SetDashAbility(GameObject player, bool enable)
    {
        player.GetComponent<CharacterDash>().AbilityPermitted = enable;
    }
}

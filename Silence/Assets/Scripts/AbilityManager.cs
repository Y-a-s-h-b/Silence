using MoreMountains.CorgiEngine;
using UnityEngine;

public enum Abilities { Jump, Sprint, Dash }

public class AbilityManager : MonoBehaviour
{
    public GameObject Player;

    public bool _sprintEnabled;
    public bool _jumpEnabled;
    public bool _dashtEnabled;

    public static AbilityManager Instance;

    private void Awake()
    {
        Instance = this;

        Player = GameObject.FindGameObjectWithTag("Player");

        _sprintEnabled = true;
        _dashtEnabled = true;
        _jumpEnabled = true;
    }

    public void EnableAbility(Abilities ability)
    {
        if (ability.Equals(Abilities.Sprint) && !_sprintEnabled)
        {
            SetSprintAbility(true);
            _sprintEnabled = true;
        }

        else if(ability.Equals(Abilities.Jump) && !_jumpEnabled)
        {
            SetJumpAbility(true);
            _jumpEnabled = true;
        }

        else if (ability.Equals(Abilities.Dash) && !_dashtEnabled)
        {
            SetDashAbility(true);
            _dashtEnabled = true;
        }
    }

    public void DisableAbility(Abilities ability)
    {
        if (ability.Equals(Abilities.Sprint) && _sprintEnabled)
        {
            SetSprintAbility(false);
            _sprintEnabled = false;
        }

        else if (ability.Equals(Abilities.Jump) && _jumpEnabled)
        {
            SetJumpAbility(false);
            _jumpEnabled = false;
        }

        else if (ability.Equals(Abilities.Dash) && _dashtEnabled)
        {
            SetDashAbility(false);
            _dashtEnabled = false;
        }
    }

    private void SetSprintAbility(bool enable)
    {
        Player.GetComponent<CharacterSprint>().AbilityPermitted = enable;
    }

    private void SetJumpAbility(bool enable)
    {
        Player.GetComponent<CharacterJump>().AbilityPermitted = enable;
    }

    private void SetDashAbility(bool enable)
    {
        Player.GetComponent<CharacterDash>().AbilityPermitted = enable;
    }
}

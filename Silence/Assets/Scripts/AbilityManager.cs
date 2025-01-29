using MoreMountains.CorgiEngine;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public GameObject Player;
    public string Dash;
    public string Jump;
    public string Sprint;

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

    public void EnableAbility(string ability)
    {
        if (ability.Equals(Sprint) && !_sprintEnabled)
        {
            SetSprintAbility(true);
            _sprintEnabled = true;
        }

        else if(ability.Equals(Jump) && !_jumpEnabled)
        {
            SetJumpAbility(true);
            _jumpEnabled = true;
        }

        else if (ability.Equals(Dash) && !_dashtEnabled)
        {
            SetDashAbility(true);
            _dashtEnabled = true;
        }
    }

    public void DisableAbility(string ability)
    {
        if (ability.Equals(Sprint) && _sprintEnabled)
        {
            SetSprintAbility(false);
            _sprintEnabled = false;
        }

        else if (ability.Equals(Jump) && _jumpEnabled)
        {
            SetJumpAbility(false);
            _jumpEnabled = false;
        }

        else if (ability.Equals(Dash) && _dashtEnabled)
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

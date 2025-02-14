using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using System;
using TMPro;
using UnityEngine;

public class CharacterMovementController : CharacterAbility, MMEventListener<MMCharacterEvent>
{
    private float timeRunButtonPressed = 0f;
    private float timeForSprint = 0f;
    public float runThresholdTime = 2f;
    public float sprintThresholdTime = 3f;
    private bool runFunOnce = true;
    private bool sprintForOnce = false;
    private CharacterHorizontalMovement charMove;
    private CharacterRun charRun;
    private CharacterSprint charSprint;
    private CharacterJump charJump;
    public TextMeshProUGUI stateTextTest;
    private CharacterStates.MovementStates prevOnFootMovementState;
    private bool startPostJump = false;
    private CorgiController controller;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.MMEventStartListening<MMCharacterEvent>();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        this.MMEventStopListening<MMCharacterEvent>();
    }
    public virtual void OnMMEvent(MMCharacterEvent characterEvent)
    {
        
        if (characterEvent.TargetCharacter.CharacterType == Character.CharacterTypes.Player)
        {
            switch (characterEvent.EventType)
            {
                case MMCharacterEventTypes.Jump:
                                   
                    Invoke("ChangeJumpPostBool", 0.5f);                    
                    break;
            }
        }
    }

    private void ChangeJumpPostBool()
    {
        if (startPostJump)
        {
            startPostJump = false;
        }
        else
        {
            startPostJump = true;
        }
    }

    protected override void Initialization()
    {
        base.Initialization();
        charMove = GetComponent<CharacterHorizontalMovement>();
        charRun = GetComponent<CharacterRun>();
        charSprint = GetComponent<CharacterSprint>();
        charJump = GetComponent<CharacterJump>();
        controller = GetComponentInParent<CorgiController>();
    }
    
    private void Update()
    {
        if (startPostJump)
        {
            if (_controller.State.IsGrounded)
            {
                AssignPrevStateAfterJump();
                ChangeJumpPostBool();
                timeRunButtonPressed = 0f;
                return;
            }
        }
        if(_character.MovementState.CurrentState == CharacterStates.MovementStates.Walking 
            || _character.MovementState.CurrentState == CharacterStates.MovementStates.Sprinting
            || _character.MovementState.CurrentState == CharacterStates.MovementStates.Running
            )
        {
            prevOnFootMovementState = _character.MovementState.CurrentState;
        }
        if(stateTextTest != null)   stateTextTest.text  =  _character.MovementState.CurrentState.ToString();
        
        if(_horizontalInput ==1 || _horizontalInput == -1)
        {
            timeRunButtonPressed += Time.deltaTime;
            if (timeRunButtonPressed >= runThresholdTime)
            {
                //changeSpeed
                if (runFunOnce)
                {
                    ChangeToRunning();
                    runFunOnce = false;
                }

                if (timeRunButtonPressed >= sprintThresholdTime && !runFunOnce && sprintForOnce)
                {
                    ChangeToSprint();

                    sprintForOnce = false;
                }
            }
        }
        else
        {
            timeRunButtonPressed = 0f;
            sprintForOnce = true;
            //charMove.MovementSpeed = charMove.WalkSpeed;            
            runFunOnce = true;
            charRun.ShouldRun = false;
            charSprint.ShouldSprint = false;
            //Debug.Log("cancle speed move");
        }
    }

    private void AssignPrevStateAfterJump()
    {
        if(prevOnFootMovementState == CharacterStates.MovementStates.Sprinting)
        {
            ChangeToSprint();
        }
        if(prevOnFootMovementState == CharacterStates.MovementStates.Running)
        {
            ChangeToRunning();
        }
        
    }

    private void ChangeToSprint()
    {
        charRun.ShouldRun = false;
        charSprint.ShouldSprint = true;
        //Debug.Log("change to sprint");
    }

    void ChangeToRunning()
    {
        charRun.ShouldRun = true;
        //Debug.Log("change to run");
    }
}

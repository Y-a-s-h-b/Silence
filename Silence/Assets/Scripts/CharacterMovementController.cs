using MoreMountains.CorgiEngine;
using System;
using TMPro;
using UnityEngine;

public class CharacterMovementController : CharacterAbility
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
    public TextMeshProUGUI stateTextTest;
    

    protected override void Initialization()
    {
        base.Initialization();
        charMove = GetComponent<CharacterHorizontalMovement>();
        charRun = GetComponent<CharacterRun>();
        charSprint = GetComponent<CharacterSprint>();
    }
    
    private void Update()
    {
        if(stateTextTest != null)   stateTextTest.text  =  _character.MovementState.CurrentState.ToString();
        if (Input.GetKeyDown(KeyCode.K))
        {
            charRun.ShouldRun = true;
        }
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

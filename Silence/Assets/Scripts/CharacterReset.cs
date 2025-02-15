using MoreMountains.CorgiEngine;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
public class CharacterReset : MonoBehaviour, MMEventListener<CorgiEngineEvent>
{
    private Character character;
    public RuntimeAnimatorController CharacterAnimator;
    public Material GhostMat;
    private Animator Animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ResetCharacter()
    {
        character = LevelManager.Instance.Players[0];
        //Animator.runtimeAnimatorController = CharacterAnimator;
        character.GetComponent<CharacterHorizontalMovement>().PermitAbility(true);
        character.GetComponent<CharacterMovementController>().sprintThresholdTime = 2;
        character.GetComponent<CharacterJump>().PermitAbility(true);
    }
    public void ResetAnimator()
    {
        character = LevelManager.Instance.Players[0];
        character.CharacterModel.GetComponent<Animator>().runtimeAnimatorController = CharacterAnimator;
        //ChangeMaterial();
    }
    public virtual void OnMMEvent(CorgiEngineEvent engineEvent)
    {
        switch (engineEvent.EventType)
        {
            case CorgiEngineEventTypes.PlayerDeath:
                ChangeMaterial();
                break;
        }
    }
    void ChangeMaterial()
    {
        if (character == null) character = LevelManager.Instance.Players[0];
        if (GhostMat == null) return;
        character.CharacterModel.GetComponent<SpriteRenderer>().material = GhostMat;
        character.CharacterModel.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.black);
        character.CharacterModel.GetComponent<SpriteRenderer>().material.SetVector("_Color", new Vector4(0,0,0,1));
    }
    protected virtual void OnEnable()
    {
        this.MMEventStartListening<CorgiEngineEvent>();
    }

    /// <summary>
    /// OnDisable, we stop listening to events.
    /// </summary>
    protected virtual void OnDisable()
    {
       this.MMEventStopListening<CorgiEngineEvent>();
    }

}

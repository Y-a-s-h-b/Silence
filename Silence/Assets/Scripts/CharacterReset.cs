using MoreMountains.CorgiEngine;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using System;
public class CharacterReset : MonoBehaviour
{
    private Character character;
    public RuntimeAnimatorController CharacterAnimator;
    private Animator Animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ResetCharacter()
    {
        character = LevelManager.Instance.Players[0];
        //Animator.runtimeAnimatorController = CharacterAnimator;
        character.GetComponent<CharacterHorizontalMovement>().PermitAbility(true);
        character.GetComponent<CharacterJump>().PermitAbility(true);
    }
    public void ResetAnimator()
    {
        character = LevelManager.Instance.Players[0];
        character.CharacterModel.GetComponent<Animator>().runtimeAnimatorController = CharacterAnimator;
    }

}

using MoreMountains.CorgiEngine;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using Demo_Project;
public class CharacterChangeMaterial : MonoBehaviour
{
    public Material GhostMat;
    private Character character;
    public Material DefaultMat;
    private SpriteRenderer spriteRenderer;
    public void ChangeMaterial(GameObject ch)
    {
        if (character == null) character = ch.GetComponent<Character>();
        if (GhostMat == null || DefaultMat == null) return;
        if (spriteRenderer == null) spriteRenderer = character.CharacterModel.GetComponent<SpriteRenderer>();
        spriteRenderer.material = GhostMat;
        spriteRenderer.material.SetColor("_Color", Color.black);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeMaterial(collision.gameObject);
        }
    }
}

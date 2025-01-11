using UnityEngine;
using MoreMountains.CorgiEngine;
using System.Collections;
using Cainos.PixelArtPlatformer_Dungeon;

public class CharacterDashBreakable : CharacterDash
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DashBreakableObstacle"))
        {
            if (_movement.CurrentState == CharacterStates.MovementStates.Dashing)
            {
                collision.gameObject.GetComponent<Door>().IsOpened = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //this breaks the obstacle
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DashBreakableObstacle"))
        {
            if (_movement.CurrentState == CharacterStates.MovementStates.Dashing)
            {
                collision.gameObject.GetComponent<Door>().IsOpened = true;
            }
        }
    }

    protected override void ComputeDashDirection()
    {
        base.ComputeDashDirection();

        _dashDirection = _character.IsFacingRight ? Vector2.right : Vector2.left;
    }
}
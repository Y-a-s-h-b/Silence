using MoreMountains.CorgiEngine;
using UnityEngine;

public class CharacterSprintBreakable : CharacterSprint
{
    public float sprintBreakDetectDistance = 5f;
    public LayerMask sprintBreakableLayer;

    public override void ProcessAbility()
    {
        base.ProcessAbility();
        ComputeBreakableCollisionRay();
    }

    private void ComputeBreakableCollisionRay()
    {
        var direction = _character.IsFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), direction, sprintBreakDetectDistance, sprintBreakableLayer); ;
        if (hit.collider != null)
        {
            if (_movement.CurrentState == CharacterStates.MovementStates.Sprinting)
            {
                hit.transform.GetComponent<Collider2D>().enabled = false;
            }
        }
        // Debug the ray in the scene view
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), direction * sprintBreakDetectDistance, Color.blue);
    }
}

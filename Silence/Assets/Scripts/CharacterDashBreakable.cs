using UnityEngine;
using MoreMountains.CorgiEngine;
using System.Collections;
using Cainos.PixelArtPlatformer_Dungeon;
using Unity.VisualScripting;

public class CharacterDashBreakable : CharacterDash
{
    public float dashBreakDetectDistance = 5f;
    public LayerMask dashBreakableLayer;
    private float cooldownTimer = 0;

    public override void ProcessAbility()
    {
        base.ProcessAbility();
        ComputeBreakableCollisionRay();
    }

    public override void StartDash()
    {
        base.StartDash();
        StartCoroutine(UpdateUI());
    }

    private void ComputeBreakableCollisionRay()
    {
        var direction = _character.IsFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position+new Vector3(0,1,0), direction, dashBreakDetectDistance, dashBreakableLayer); ;
        if (hit.collider != null)
        {
            if (_movement.CurrentState == CharacterStates.MovementStates.Dashing)
            {
                hit.transform.GetComponent<Collider2D>().enabled = false;
                hit.transform.gameObject.GetComponent<Door>().IsOpened = true;                

            }
            Debug.Log("collided with obstacle");
        }
        // Debug the ray in the scene view
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), direction * dashBreakDetectDistance, Color.red);
    }

    IEnumerator UpdateUI()
    {
        bool isCompleted = false;
        while (!isCompleted)
        {
            if (GUIManager.HasInstance)
            {
                float currentTimer = Time.time - _startTime;
                GUIManager.Instance.UpdateDashBar(DashCooldown - currentTimer, 0f, DashCooldown, _character.PlayerID);

                if (currentTimer >= DashCooldown)
                {
                    isCompleted = true;
                }
            }

            yield return null;
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("in");
        if (collision.gameObject.layer == LayerMask.NameToLayer("DashBreakableObstacle"))
        {
            if (_movement.CurrentState == CharacterStates.MovementStates.Dashing)
            {
                Debug.Log("in");
                collision.gameObject.GetComponent<Door>().IsOpened = true;
                collision.gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //this breaks the obstacle
    {

        Debug.Log("in");
        if (collision.gameObject.layer == LayerMask.NameToLayer("DashBreakableObstacle"))
        {
            if (_movement.CurrentState == CharacterStates.MovementStates.Dashing)
            {
                collision.gameObject.GetComponent<Door>().IsOpened = true;
            }
        }
    }
    */
    protected override void ComputeDashDirection()
    {
        base.ComputeDashDirection();

        _dashDirection = _character.IsFacingRight ? Vector2.right : Vector2.left;
    }
}
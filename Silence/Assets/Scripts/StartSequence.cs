using System.Collections;
using System.Net;
using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class StartSequence : MonoBehaviour
{
    public Character character;
    public GameObject cameraTarget;
    public CinemachineCamera camera;
    public float duration = 2f;
    private float elapsedTime = 0f;
    public GameObject npc; // Assign NPC GameObject
    public Animator npcAnimator;
    public GameObject jailDoor; // Assign jail door with Animator
    public Transform jailDoorPosition; // Position to stop at before opening
    public Animator jailDoorAnimator; // Assign the door's animator
    public int speed;
    public DialogueZone npcDialogue;
    public Character player;
    public Transform killer;
    public bool FollowPlayer = false;
    public MMF_Player playerFeedback;
    void Start()
    {
        InputManager.Instance.InputDetectionActive = false; 
        npcAnimator = npc.GetComponentInChildren<Animator>();
        npcDialogue = npc.GetComponentInChildren<DialogueZone>();
        player = LevelManager.Instance.Players[0];
        StartCoroutine(StartSequenceCo());

    }
    void Update()
    {
        if(player != null && Vector2.Distance(player.transform.position, killer.position) > 0.1f)
        {
            FollowPlayer = true;
            if(InputManager.Instance.InputDetectionActive && player.MovementState.CurrentState == CharacterStates.MovementStates.Idle)
            {
                npcAnimator.SetBool("Idle", true);
            }
            if(InputManager.Instance.InputDetectionActive && player.MovementState.CurrentState != CharacterStates.MovementStates.Idle)
            {
                npcAnimator.SetBool("Idle", false);
            }
        }
    }
    IEnumerator StartSequenceCo()
    {
        npcAnimator.SetTrigger("Walk");
        // NPC Walks to Jail Door
        while (Vector2.Distance(npc.transform.position, jailDoorPosition.position) > 0.1f)
        {
            npc.transform.position = Vector2.MoveTowards(npc.transform.position, jailDoorPosition.position, speed * Time.deltaTime);
            //camera.transform.position = Vector2.MoveTowards(camera.transform.position,
            camera.GetComponent<CinemachineFollow>().FollowOffset = new Vector3(-8, 5, -10);
            camera.Follow = npc.transform;
            yield return null;
        }
        npcAnimator.SetTrigger("Open");
        playerFeedback.PlayFeedbacks(this.transform.position);

        yield return new WaitForSeconds(.25f);

        StartCoroutine(ShowMoveIcon());
        jailDoorAnimator.SetBool("IsOpened",true);
        npcDialogue.StartDialogue();

        yield return new WaitForSeconds(1.5f);

        InputManager.Instance.InputDetectionActive = true;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.D));
        player = LevelManager.Instance.Players[0];
        camera.GetComponent<CinemachineFollow>().FollowOffset = new Vector3(2, 5, -10);
        camera.Follow = player.CameraTarget.transform;
        player.CharacterAnimator.SetBool("Vibing",false);
        yield return new WaitUntil(() => (npc.transform.position.x < player.transform.position.x));
        while (Vector2.Distance(npc.transform.position, player.transform.position) > 0.1f && FollowPlayer)
        {
            npcAnimator.SetTrigger("Walk");
            npc.GetComponentInChildren<SpriteRenderer>().gameObject.transform.rotation = player.CharacterModel.transform.rotation;
            npc.transform.position = Vector2.MoveTowards(npc.transform.position, player.transform.position + new Vector3(-2,0,0), speed+2 * Time.deltaTime);
            yield return null;
        }
    }
    public void ShootSequence()
    {

    }
    IEnumerator ShowMoveIcon()
    {
        yield return new WaitForSeconds(3);
        player.GetComponentInChildren<DialogueZone>().ShowPrompt();
    }
}

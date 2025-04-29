using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using MoreMountains.CorgiEngine;
using UnityEditor.Animations;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;
using Unity.Cinemachine;
using Demo_Project;

public class CheatConsoleManager : MonoBehaviour
{
    public float inputResetTime = 2f; // reset if idle too long
    private string currentInput = "";
    private float lastInputTime;

    // Optional: Assign MMFeedbacks for popup message
    public MMF_Player feedbackPopup;
    public string popupText = "Cheat Activated!";

    private Dictionary<string, System.Action<bool>> cheatCommands;
    private Dictionary<string, bool> cheatStates = new Dictionary<string, bool>();
    public AnimatorController playerAnimator;
    public Weapon WeaponToGive;
    private int currentCheckpointIndex = 0;

    private void Start()
    {
        cheatCommands = new Dictionary<string, System.Action<bool>>()
        {
            { "unlock", UnlockAllAbilities },
            { "godmode", EnableGodMode },
            { "money", AddMoney },
            { "next", GoToNextCheckpoint },
            { "prev", GoToPreviousCheckpoint },
            { "skip", SkipCutscene }
        };
    }

    private void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c))
            {
                currentInput += c;
                lastInputTime = Time.time;
            }
        }

        // Reset if idle
        if (Time.time - lastInputTime > inputResetTime)
        {
            currentInput = "";
        }

        foreach (var cheat in cheatCommands)
        {
            if (currentInput.ToLower().Contains(cheat.Key))
            {
                bool isActive = cheatStates.ContainsKey(cheat.Key) && cheatStates[cheat.Key];

                cheat.Value.Invoke(isActive);
                cheatStates[cheat.Key] = !isActive;

                ShowPopup($"Cheat: {cheat.Key.ToUpper()} {(isActive ? "deactivated" : "activated")}");
                currentInput = "";
                break;
            }
        }

    }

    private void UnlockAllAbilities(bool isActive)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        if(playerAnimator != null) player.GetComponentInChildren<Animator>().runtimeAnimatorController = playerAnimator;
        var character = player.GetComponent<MoreMountains.CorgiEngine.Character>();
        if (isActive)
        {
            // Disable abilities
            var dash = player.GetComponentInChildren<CharacterDash>();
            if (dash != null) dash.AbilityPermitted = false;
            
            var characterHandleWeapon = player.GetComponentInChildren<CharacterHandleWeapon>();
            if (characterHandleWeapon != null)
            {
                characterHandleWeapon.ChangeWeapon(null, null);
                //WeaponManager.Instance.StoreWeapon(character, WeaponToGive, playerAnimator);
            }
            var jump = player.GetComponentInChildren<CharacterJump>();
            if (jump != null) jump.NumberOfJumps = 1;

            Debug.Log("Cheat Deactivated: Abilities reset.");
        }
        else
        {
            // Enable abilities
            var dash = player.GetComponentInChildren<CharacterDash>();
            if (dash != null) dash.AbilityPermitted = true;

            var jump = player.GetComponentInChildren<CharacterJump>();
            if (jump != null)
            {
                jump.AbilityPermitted = true;
                jump.NumberOfJumps = 2;
            }
            var characterHandleWeapon = player.GetComponentInChildren<CharacterHandleWeapon>();
            if (characterHandleWeapon != null)
            {
                characterHandleWeapon.ChangeWeapon(WeaponToGive, null);
                WeaponManager.Instance.StoreWeapon(character, WeaponToGive, playerAnimator); 
                foreach (var bar in GUIManager.Instance.weaponCooldownBars)
                {
                    bar.gameObject.SetActive(true);
                }
            }
            Debug.Log("Cheat Activated: Dash enabled, double jump unlocked.");
        }
    }

    private void EnableGodMode(bool isActive)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var health = player.GetComponent<Health>();
            if (health != null)
            {
                health.Invulnerable = !isActive;
                Debug.Log("God Mode: " + (!isActive));
            }
        }
    }

    private void AddMoney(bool isActive)
    {
        if (!isActive) // Only give money once, not on disable
        {
			CorgiEnginePointsEvent.Trigger(PointsMethods.Add, 100);
            Debug.Log("Cheat: Added 1000 money");
            // CurrencyManager.Instance.Add(1000);
        }
    }


    private void ShowPopup(string message)
    {
        if (feedbackPopup != null)
        {
            // Set popup text (if using MMFeedbackText)
            var textFeedback = feedbackPopup.GetFeedbackOfType<MMF_TMPText>();
            if (textFeedback != null)
            {
                textFeedback.NewText = message;
            }

            feedbackPopup.PlayFeedbacks();
        }
    }
    private void GoToNextCheckpoint(bool isActive)
    {
        var levelManager = LevelManager.Instance;
        if (levelManager == null || levelManager.Checkpoints == null || levelManager.Checkpoints.Count == 0) return;

        currentCheckpointIndex = Mathf.Clamp(currentCheckpointIndex + 1, 0, levelManager.Checkpoints.Count - 1);
        TeleportToCheckpoint(levelManager.Checkpoints[currentCheckpointIndex]);

        Debug.Log("Teleported to next checkpoint.");
    }

    private void GoToPreviousCheckpoint(bool isActive)
    {
        var levelManager = LevelManager.Instance;
        if (levelManager == null || levelManager.Checkpoints == null || levelManager.Checkpoints.Count == 0) return;

        currentCheckpointIndex = Mathf.Clamp(currentCheckpointIndex - 1, 0, levelManager.Checkpoints.Count - 1);
        TeleportToCheckpoint(levelManager.Checkpoints[currentCheckpointIndex]);

        Debug.Log("Teleported to previous checkpoint.");
    }

    private void TeleportToCheckpoint(CheckPoint checkpoint)
    {
        var levelManager = LevelManager.Instance;
        if (levelManager == null || checkpoint == null) return;

        levelManager.SetCurrentCheckpoint(checkpoint);

        // Force respawn at the new checkpoint
        levelManager.CurrentCheckPoint.SpawnPlayer(levelManager.Players[0]);
    }

    private void SkipCutscene(bool isActive)
    {
        var camera = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineCamera;
        var player = GameObject.FindGameObjectWithTag("Player");
        var character = player.GetComponent<MoreMountains.CorgiEngine.Character>();
        var sequence = FindAnyObjectByType<StartSequence>();
        if (sequence != null) { sequence.transform.parent.gameObject.SetActive(false); }
        var levelManager = LevelManager.Instance;
        var inputManager = InputManager.Instance;
        if (levelManager == null || levelManager.Checkpoints == null || levelManager.Checkpoints.Count == 0) return;
        if(CrystalHandler.Instance != null) CrystalHandler.Instance.currentShardIndex++;

        if (playerAnimator != null) player.GetComponentInChildren<Animator>().runtimeAnimatorController = playerAnimator;
        var characterHandleWeapon = player.GetComponentInChildren<CharacterHandleWeapon>();
        if (characterHandleWeapon != null)
        {
            characterHandleWeapon.ChangeWeapon(WeaponToGive, null);
            WeaponManager.Instance.StoreWeapon(character, WeaponToGive, playerAnimator);
        }
        var jump = player.GetComponentInChildren<CharacterJump>();
        if (jump != null)
        {
            jump.AbilityPermitted = true;
        } 
        var audioHealth = player.GetComponentInChildren<AudioHealthController>();
        if (audioHealth != null)
        {
            audioHealth.scriptEnabled = true;
            audioHealth.dieable = true;
        }
        var movement = player.GetComponentInChildren<CharacterMovementController>();
        if (movement != null) movement.PermitAbility(true);
        character.GetComponent<CharacterMovementController>().sprintThresholdTime = 1.5f;
        inputManager.InputDetectionActive = true;
        GUIManager.Instance.SetHUDActive(true);
        camera.GetComponent<CinemachineCameraController>().TargetCharacter = character;
        camera.GetComponent<CinemachineFollow>().FollowOffset = new Vector3(2, 4, -10);
        camera.Follow = character.CameraTarget.transform;
        currentCheckpointIndex = 2;
        foreach (var bar in GUIManager.Instance.weaponCooldownBars)
        {
            bar.gameObject.SetActive(true);
        }
        TeleportToCheckpoint(levelManager.Checkpoints[currentCheckpointIndex]); 
       
    }
}

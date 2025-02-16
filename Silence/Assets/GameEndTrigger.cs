using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;
using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    public MMF_Player EndFeedback;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (CrystalHandler.Instance.currentShardIndex < 4)
            {
                GetComponent<DialogueZone>().StartDialogue();
            }
            else
            {
                EndFeedback.PlayFeedbacks();
            }
        }
    }
}

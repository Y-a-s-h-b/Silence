using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;

public class EnableDoubleJump : MonoBehaviour
{
    public MMF_Player CollectFeedback;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterJump>().NumberOfJumps =2;
            CollectFeedback.PlayFeedbacks();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterJump>().NumberOfJumps = 2;
            CollectFeedback.PlayFeedbacks();
            Destroy(gameObject);
        }
    }
}

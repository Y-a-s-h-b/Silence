using UnityEngine;
using MoreMountains.CorgiEngine;

public class EnableDoubleJump : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterJump>().NumberOfJumps =2;
            Destroy(gameObject);
        }
    }
}

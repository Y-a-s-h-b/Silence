using UnityEngine;

public class PlayerInHell : MonoBehaviour
{
    public bool inHell;

    private void Start()
    {
        inHell = false;
    }

   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inHell = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject;
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<AudioHealthController>().dieable = false;
            player.GetComponent<AudioHealthController>().scriptEnabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inHell = false;
            var player = collision.gameObject;
            if (collision.gameObject.CompareTag("Player"))
            {
                player.GetComponent<AudioHealthController>().dieable = true;
                player.GetComponent<AudioHealthController>().scriptEnabled = true;
            }
        }
    }
}

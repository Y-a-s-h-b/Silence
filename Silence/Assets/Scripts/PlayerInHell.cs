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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inHell = false;
        }
    }
}

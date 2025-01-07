using Cainos.PixelArtPlatformer_Dungeon;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Door>().IsOpened = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<Door>().IsOpened = false;
    }
}

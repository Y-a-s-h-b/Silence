using UnityEngine;

public class RotatteOnClick : MonoBehaviour
{
    private bool isActive = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isActive)
            {
                isActive = false;
                gameObject.transform.Rotate(0, 180, 0);
            }
        }
    }
}

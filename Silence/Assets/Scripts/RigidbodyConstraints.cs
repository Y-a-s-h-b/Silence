using UnityEngine;

public class RigidbodyConstraints : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyConstraint()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void RemoveConstraint()
    {
        rb.constraints = RigidbodyConstraints2D.None;
    }
}

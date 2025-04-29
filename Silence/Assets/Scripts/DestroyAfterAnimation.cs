using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    private Animator animator;
    private float animationLength;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null || animator.runtimeAnimatorController == null)
        {
            Debug.LogWarning("No Animator or RuntimeAnimatorController found.");
            return;
        }

        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        animationLength = clipInfo[0].clip.length;
        Invoke(nameof(DestroySelf), animationLength);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}

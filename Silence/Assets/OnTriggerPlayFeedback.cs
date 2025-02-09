using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

public class OnTriggerPlayFeedback : MMSingleton<OnTriggerPlayFeedback>
{
    public bool m_IsPlaying = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_IsPlaying)
        {
            MMF_Player mMF_Player = GetComponent<MMF_Player>();
            mMF_Player.PlayFeedbacks();
            m_IsPlaying=true;
        }
    }
}

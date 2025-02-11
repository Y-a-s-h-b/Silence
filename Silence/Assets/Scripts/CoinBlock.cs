using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinBlock : MonoBehaviour
{
    public bool increase = false;
    public float pitchToAdd = 0;
    private IEnumerable<MMF_Sound> coinFeedbackSounds;
    private List<MMF_Sound> players;

    private void Awake()
    {
        coinFeedbackSounds = gameObject.GetComponentsInChildren<Coin>().Select(coin => (coin.PickFeedbacks as MMF_Player).FeedbacksList.OfType<MMF_Sound>().First());
    }

    public void AddToPitch()
    {
        foreach (var coinFeedbackSound in coinFeedbackSounds)
        {
            if (coinFeedbackSound != null)
            {
                if (increase)
                {
                    coinFeedbackSound.MaxPitch += pitchToAdd;
                }
                else
                {
                    coinFeedbackSound.MaxPitch -= pitchToAdd;
                }
            }
        }
    }
}

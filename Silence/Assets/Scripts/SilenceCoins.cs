using MoreMountains.CorgiEngine;
using UnityEngine;

public class SilenceCoins : Coin
{
    public CoinBlock coinBlock { get; set; }

    private void Awake()
    {
        coinBlock = gameObject.GetComponentInParent<CoinBlock>();
    }

    protected override void Pick(GameObject picker)
    {
        base.Pick(picker);
        coinBlock.AddToPitch();
    }
}

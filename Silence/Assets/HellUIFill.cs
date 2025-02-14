using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using RengeGames.HealthBars;
using UnityEngine;


public class HellUIFill : MonoBehaviour
{
    public RadialSegmentedHealthBar fillBar;
    public Sacrifice sacrifice;
    private SpriteRenderer SpriteRenderer;
    private void Start()
    {
        ResetFill();
       
    }
    // Update is called once per frame
    void Update()
    {
        if(QTEManager.Instance != null && QTEManager.Instance.holding)
        {
            if(fillBar.RemoveSegments.Value < 0)
            {
                fillBar.RemoveSegments.Value = 1;
            }
            SpriteRenderer = sacrifice.player.GetComponent<Character>().CharacterModel.GetComponent<SpriteRenderer>();
            Color newColor = Color.Lerp(Color.black, Color.white, QTEManager.Instance.holdTime/2);
            SpriteRenderer.material.SetColor("_Color", newColor);
            SpriteRenderer.material.SetFloat("_GhostBlend", .95f - QTEManager.Instance.holdTime / 2);
            fillBar.RemoveSegments.Value = 1 - QTEManager.Instance.holdTime/2;

        }
    }
    public void ResetFill()
    {
        fillBar.RemoveSegments.Value = 1;
    }
}

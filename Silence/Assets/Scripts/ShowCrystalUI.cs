using MoreMountains.MMInterface;
using UnityEngine;

public class ShowCrystalUI : MonoBehaviour
{
    public void ShowUI()
    {
        CrystalHandler.Instance.GetComponent<CanvasGroup>().alpha = 1;
        CrystalHandler.Instance.CollectShard();
    }
}

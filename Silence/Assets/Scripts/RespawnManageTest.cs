using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class RespawnManageTest : MonoBehaviour, MMEventListener<CorgiEngineEvent>
{
    private void OnEnable() => this.MMEventStartListening<CorgiEngineEvent>();
    private void OnDisable() => this.MMEventStopListening<CorgiEngineEvent>();
    public void OnMMEvent(CorgiEngineEvent eventType)
    {
        if (eventType.EventType == CorgiEngineEventTypes.GameOver)
        {
            Debug.Log("gameover");
        }
        if (eventType.EventType == CorgiEngineEventTypes.PlayerDeath)
        {
            Debug.Log("death");
        }
    }

}

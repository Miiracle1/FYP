using UnityEngine;

public partial class SpiderAI
{
    public Phase2Points[] SpiderPhase2Points { get; private set; }

    public Phase2Points currentPoint { get; private set;}

    /// <summary>
    /// Gets phase 2 point cache.
    /// Put in start or awake.
    /// </summary>
    private void CachePhase2Points()
    {
        SpiderPhase2Points = FindObjectsByType<Phase2Points>(FindObjectsSortMode.None);
    }

    public void SetCurrentTPPoint(Phase2Points point)
    { 
        currentPoint = point;
    }

}

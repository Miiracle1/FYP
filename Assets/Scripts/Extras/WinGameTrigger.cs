using UnityEngine;

/// <summary>
/// A trigger box set near door to set win state
/// </summary>
public class WinGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                if (player.GetAttachingSpider() == true || player.IsGrabbingSpider)
                {
                    GameProgressTracker.GameState = GameStateEnums.Victory;
                }
            }
        }
    }
}

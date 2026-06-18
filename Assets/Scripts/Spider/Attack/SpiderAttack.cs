using UnityEngine;

/// <summary>
/// Attach on Attack animation attack key frame or attack collider to initiate player take damage logic.
/// </summary>
public class SpiderAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerHealth>(out var health))
            {
                health.TakeDamage();
            }
        }*/
    }
}

using UnityEngine;

/// <summary>
/// Put in Garage scene each rooms to track which room spider is in.
/// </summary>
public class SpiderRoomCollider : MonoBehaviour
{
    public Collider roomBound;

    private void Awake()
    {
        if (roomBound == null)
            roomBound = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SpiderAI>(out var spider))
        {
            spider.currentRoom = this;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<SpiderAI>(out var spider))
        {
            if (spider.currentRoom != this)
            {
                spider.currentRoom = this;
            }
        }
    }
}

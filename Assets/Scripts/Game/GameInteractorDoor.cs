using UnityEngine;

public class GameInteractorDoor : MonoBehaviour
{
    [SerializeField] private OcclusionPortal portal;
    [SerializeField] private float checkTime = 0.1f;
    [SerializeField] private GameObject door;

    private Transform originalDoorTransform;
    private bool playerEnter;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalDoorTransform = door.transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer > 0f)
            return;

        timer = checkTime;

        if (door.transform.position != originalDoorTransform.position)
        {
            SetOpen();
        }
        else
            SetClose();

        timer += Time.deltaTime;
    }

    private void SetOpen()
    {
        if (portal == null) return;

        portal.open = true;
    }

    private void SetClose()
    {
        if (portal == null) return;

        portal.open = false;
    }
}

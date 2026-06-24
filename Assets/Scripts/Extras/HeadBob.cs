using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] private Transform cameraOffset;
    [SerializeField] private float bobSpeed = 6f;
    [SerializeField] private float bobAmount = 0.015f;

    private Vector3 initialPos;
    private float timer;

    private void Start()
    {
        initialPos = cameraOffset.localPosition;
    }

    public void UpdateBob(bool isWalking)
    {
        if (isWalking)
        {
            timer += Time.deltaTime * bobSpeed;

            cameraOffset.localPosition = initialPos +
                Vector3.up * Mathf.Sin(timer) * bobAmount;
        }
        else
        {
            timer = 0;
            cameraOffset.localPosition = Vector3.Lerp(
                cameraOffset.localPosition,
                initialPos,
                Time.deltaTime * 8f);
        }
    }
}

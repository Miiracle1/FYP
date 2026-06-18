using UnityEngine;

public class SpiderPetCollider : MonoBehaviour
{
    public SpiderPartEnums spiderEnums;

    private SpiderPet petDetection;

    private void Awake()
    {
        petDetection = GetComponent<SpiderPet>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Left_Controller") || other.CompareTag("Right_Controller"))
        {
            petDetection.RegisterHand(other.transform, spiderEnums);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Left_Controller") || other.CompareTag("Right_Controller"))
        {
            petDetection.UnregisterHand(other.transform, spiderEnums);
        }
    }
}

using UnityEngine;

/// <summary>
/// Handles Petting Logic and on Trigger logic
/// </summary>
public partial class SpiderAI
{
    private bool leftHandTouching;
    private bool rightHandTouching;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Left_Controller"))
        { 
            leftHandTouching = true;
        }

        if (other.CompareTag("Right_Controller"))
        { 
            rightHandTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Left_Controller"))
        {
            leftHandTouching = false;
        }

        if (other.CompareTag("Right_Controller"))
        {
            rightHandTouching = false;
        }
    }

    private void DetectPetting()
    { 
        
    }

}

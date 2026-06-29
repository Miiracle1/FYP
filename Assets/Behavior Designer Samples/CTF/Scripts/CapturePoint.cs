using UnityEngine;

namespace BehaviorDesigner.Samples
{
    // Notifies the phase1 manager when the flag enters the trigger
    public class CapturePoint : MonoBehaviour
    {
        // the flag's phase1 object
        public GameObject flag;

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.Equals(flag)) {
                // When the flag reaches the capture point the phase1 is over
                CTFGameManager.instance.resetGame();
            }
        }
    }
}
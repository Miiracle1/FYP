using UnityEngine;

namespace BehaviorDesigner.Samples
{
    // The flag will reparent itself if it comes in contact with an object who is tagged with the offense tag
    public class Flag : MonoBehaviour
    {
        // The tag that can pick the flag up
        public string offenseTag;

        public void OnTriggerEnter(Collider other)
        {
            NPC npc = null;
            if ((npc = other.GetComponent<NPC>()) != null) {
                // the colliding object must be a NPC on offense
                if (npc.IsOffense) {
                    // notify the phase1 manager
                    if (CTFGameManager.instance.flagTaken()) {
                        // the phase1 manager says that it is ok to take the flag
                        transform.parent = other.transform;
                        // the phase1 object taking the flag will have a NPC component, let that component know that it has the flag
                        other.GetComponent<NPC>().HasFlag = true;
                    }
                }
            }
        }
    }
}
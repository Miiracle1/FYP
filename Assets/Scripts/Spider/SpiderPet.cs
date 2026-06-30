using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Detects player pet action then invokes main lobbySpider script to execute logic.
/// Also detects catch interaction.
/// </summary>
public class SpiderPet : MonoBehaviour
{
    [Header("Pet Settings")]
    [SerializeField] private float petSpeedThreshold = 0.1f; // Small threshold to determine confirmed petting action
    [SerializeField] private float requiredPetTime = 0.1f; // Require certain amount of time to count as pet action

    private float petTimer;
    private SpiderAI spider;
    private bool isPetting = false;

    public bool IsPetting { get { return isPetting; } set { isPetting = value; } } // Have a public bool to get reference

    private readonly Dictionary<Transform, HandPetData> trackedHands = new();

    private void Awake()
    {
        spider = GetComponent<SpiderAI>();
    }

    void Update()
    {
        if (trackedHands.Count == 0)
        {
            isPetting = false;
            return;
        }

        bool anyPetting = false;

        foreach (var pair in trackedHands.ToList())
        {
            Transform hand = pair.Key;
            var data = pair.Value;

            float speed = Vector3.Distance(hand.position, data.PreviousPosition) / Time.deltaTime;

            if (speed > petSpeedThreshold)
            {
                data.PetTimer += Time.deltaTime;
            }
            else
            {
                data.PetTimer -= Time.deltaTime * 2f;
            }

            data.PetTimer = Mathf.Clamp(data.PetTimer, 0f, requiredPetTime);

            if (data.PetTimer >= requiredPetTime)
            {
                anyPetting = true;
            }

            data.PreviousPosition = hand.position;
            trackedHands[hand] = data;
        }

        isPetting = anyPetting;
    }

    public void RegisterHand(Transform hand, SpiderPartEnums part)
    {
        if (!trackedHands.ContainsKey(hand))
        {
            trackedHands.Add(hand, new HandPetData 
            {
                PreviousPosition = hand.position,
                PetTimer = 0f,
                CurrentBodyPart = part,
            });
        }
    }

    public void UnregisterHand(Transform hand, SpiderPartEnums part)
    {
        trackedHands.Remove(hand);
    }
}

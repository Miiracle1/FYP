using System;
using UnityEngine;

/// <summary>
/// Handles gameplay lobbySpider behavior logic
/// </summary>
public partial class SpiderAI
{
    [Header("In Game Settings")]
    [SerializeField] private Transform[] possiblePositions; // store transforms of possible locations of lobbySpider

    private bool phase1; // flag to check if the lobbySpider is in phase1 level not lobby
    private bool phase2 = false;

    private bool isGrabbed = false;
    private bool isAttached = false;
    public SpiderRoomCollider currentRoom;

    public bool spottedPlayer = false; //extra bool to use in phase 1
    public bool justEnteredPhase2 = false;
    public static event Action OnPhase2Start;
    public static event Action OnPhase2End;

    public bool InGamePhase1
    {
        get => phase1;
    }

    public bool InGamePhase2
    {
        get => phase2;
    }

    public bool IsGrabbed { get => isGrabbed; }

    public bool IsAttached { get => isAttached; }

    private void StartPhase1()
    {
        phase1 = true;
    }

    public void SetPhase2()
    { 
        phase1 = false;
        justEnteredPhase2 = true;
    }

    public void EnterPhase2()
    {
        spottedPlayer = false;
        justEnteredPhase2 = false;
        phase2 = true;
        OnPhase2Start?.Invoke();
    }

    public void SetEndPhase2()
    {
        phase2 = false;
        OnPhase2End?.Invoke();
    }

    public void SetGrabbed(bool value)
    { 
        isGrabbed = value;
    }

    public void SetAttached(bool value) 
    { 
        isAttached = value; 
    }
}

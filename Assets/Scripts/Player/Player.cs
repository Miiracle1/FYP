using UnityEngine;

public partial class Player : MonoBehaviour
{
    void Start()
    {
        InitializeHealth();
    }
}

public enum PlayerStateEnums
{ 
    Alive,
    Dead,
    Respawning
}

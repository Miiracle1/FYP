using UnityEngine;

public partial class Player : MonoBehaviour
{
    

    private void Awake()
    {
        
    }

    void Start()
    {
        InitializeHealth();
    }

    void Update()
    {
        
    }
}

public enum PlayerStateEnums
{ 
    Alive,
    Dead,
    Respawning
}

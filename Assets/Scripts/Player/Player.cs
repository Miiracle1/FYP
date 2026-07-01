using UnityEngine;

public partial class Player : MonoBehaviour
{
    private bool isAttachingSpider = false;

    /***************************************************************************************************************************************************************************************/
    // Unity Methods

    void Start()
    {
        InitializeHealth();
    }

    /***************************************************************************************************************************************************************************************/
    // Methods

    public bool GetAttachingSpider()
    { 
        return isAttachingSpider;
    }

    public void SetAttachingSpider(bool value)
    { 
        isAttachingSpider = value;
    }
}

/***************************************************************************************************************************************************************************************/
// Player Enums

public enum PlayerStateEnums
{ 
    Alive,
    Dead,
    Respawning
}

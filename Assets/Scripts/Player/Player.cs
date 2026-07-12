using UnityEngine;

public partial class Player : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float FOV = 0.6f;

    private bool isAttachingSpider = false;
    private bool isGrabbingSpider = false;

    public bool IsGrabbingSpider
    {  
        get { return isGrabbingSpider; } 
        set { isGrabbingSpider = value; }
    }

    /***************************************************************************************************************************************************************************************/
    // Unity Methods

    void Start()
    {
        InitializeHealth();
        AdjustFOV();
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

    private void AdjustFOV()
    { 
        this.transform.localScale = Vector3.one * FOV;
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

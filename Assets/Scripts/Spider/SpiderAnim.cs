using UnityEngine;

public partial class SpiderAI
{
    [Header("Animation")]
    [SerializeField] private AnimationClip idleClip;
    [SerializeField] private AnimationClip movingClip;

    /***************************************************************************************************************************************************************************************/
    //Public Methods

    /// <summary>
    /// Call to play idle animation.
    /// Sets move boolean to false.
    /// </summary>
    public void PlayIdleAnim()
    {
        PlayIdle();
    }

    /// <summary>
    /// Call to play move animation.
    /// Sets idle boolean to false.
    /// </summary>
    public void PlayMoveAnim()
    { 
        PlayMove();
    }

    /***************************************************************************************************************************************************************************************/
    //Private Methods

    private void PlayIdle()
    {
        if (idleClip != null)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
        }
    }

    private void PlayMove()
    {
        if (movingClip != null)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Move", true);
        }
    }
}

using UnityEngine;

public partial class SpiderAI
{
    [Header("Animation")]
    [SerializeField] private AnimationClip idleClip;
    [SerializeField] private AnimationClip movingClip;

    /***************************************************************************************************************************************************************************************/
    //Public Methods

    /// <summary>
    /// Call to play wait animation.
    /// Sets move boolean to false.
    /// </summary>
    public void PlayWaitingAnim()
    {
        PlayWaiting();
    }

    /// <summary>
    /// Call to play move animation.
    /// Sets idle boolean to false.
    /// </summary>
    public void PlayMoveAnim()
    { 
        PlayMove();
    }

    /// <summary>
    /// Call to stop idle animation.
    /// Sets idle boolean only to false.
    /// </summary>
    public void StopWaitingAnim()
    { 
        StopWaiting();
    }

    /// <summary>
    /// Call to stop move animation.
    /// Sets move boolean only to false.
    /// </summary>
    public void StopMoveAnim()
    { 
        StopMove();
    }

    public void StopAllAnim()
    {
        StopWaiting();
        StopMove();
    }

    /***************************************************************************************************************************************************************************************/
    //Private Methods

    private void PlayWaiting()
    {
        animator.SetBool("Wait", true);
        animator.SetBool("Move", false);
    }

    private void PlayMove()
    {
        animator.SetBool("Move", true);
        animator.SetBool("Wait", false);
    }

    private void StopWaiting()
    {
        animator.SetBool("Wait", false);
    }

    private void StopMove()
    {
        animator.SetBool("Move", false);
    }
}

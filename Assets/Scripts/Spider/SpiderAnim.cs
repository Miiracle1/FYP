using UnityEngine;

public partial class SpiderAI
{
    [Header("Animation")]
    [SerializeField] private AnimationClip idleClip;
    [SerializeField] private AnimationClip movingClip;

    [Header("Sockets")]
    [SerializeField] private GameObject headSocket;

    /***************************************************************************************************************************************************************************************/
    //Public Methods

    /// <summary>
    /// Call to play wait animation.
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
        StopMove();
    }

    public bool GetMoveBool()
    {
        return animator.GetBool("Move");
    }

    /***************************************************************************************************************************************************************************************/
    //Private Methods

    private void PlayIdle()
    {
        animator.SetTrigger("Idle");
        animator.SetBool("Move", false);
    }

    private void PlayMove()
    {
        animator.SetBool("Move", true);
    }

    private void StopMove()
    {
        animator.SetBool("Move", false);
    }

    /***************************************************************************************************************************************************************************************/
    //Sockets

    public GameObject GetHeadSocket()
    {
        return headSocket;
    }
}

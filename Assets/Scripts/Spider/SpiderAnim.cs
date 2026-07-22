using UnityEngine;
using Animancer;

public partial class SpiderAI
{
    [Header("Animation")]
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private AnimationClip idleClip;
    [SerializeField] private AnimationClip movingClip;

    [Header("Sockets")]
    [SerializeField] private GameObject headSocket;

    private AnimationClip currentClip; 

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
        //ResetIdleTrigger();
        StopMove();
    }


    public void ResetIdle()
    { 
        //ResetIdleTrigger();
    }

    public void Play(AnimationClip clip)
    {
        if (currentClip == clip )
        {
            return;
        }

        currentClip = clip;
        animancer.Play(clip);
    }

    private void PlayOneShot(AnimationClip clip)
    {
        currentClip = null;

        animancer.Play(clip);
    }

    public void ResetClip()
    {
        currentClip = null;
    }

    /***************************************************************************************************************************************************************************************/
    //Private Methods

    private void PlayIdle()
    {
        PlayOneShot(idleClip);
    }

    private void PlayMove()
    {
        Play(movingClip);
    }

    private void StopMove()
    {
        PlayIdleAnim();
    }

    private void ResetIdleTrigger()
    {
        //animator.ResetTrigger("Idle");
    }

    /***************************************************************************************************************************************************************************************/
    //Sockets

    public GameObject GetHeadSocket()
    {
        return headSocket;
    }
}

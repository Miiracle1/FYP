using UnityEngine;
using Animancer;
using System.Collections;

public partial class SpiderAI
{
    [Header("Animation")]
    public AnimancerComponent animancer;
    [SerializeField] private AnimationClip idleClip;
    [SerializeField] private AnimationClip movingClip;

    [Header("Sockets")]
    [SerializeField] private GameObject headSocket;

    [Header("Idle")]
    [SerializeField] private float idleCheckInterval = 1f;
    [SerializeField] private float idleChance = 20f;

    private AnimationClip currentClip;
    private float nextIdleCheckTime;
    private bool specialMove = false;
    public bool SpecialMove
    {
        get { return specialMove; }
        set { specialMove = value; }
    }

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
        if (currentClip == clip) return;

        currentClip = clip;
        animancer.Play(clip);
    }

    private void PlayOneShot(AnimationClip clip)
    {
        currentClip = clip;

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
        if (animancer.States.Current?.Clip == movingClip)
        {
            currentClip = null;
            animancer.Stop(movingClip);
        }
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

    /***************************************************************************************************************************************************************************************/
    //Random Idle

    /// <summary>
    /// Put in Update.
    /// </summary>
    private void RandomIdle()
    {
        var cantIdle = agent.velocity.magnitude > 0.1f;

        if (cantIdle) return;

        if (Time.time < nextIdleCheckTime) return;

        nextIdleCheckTime = Time.time + idleCheckInterval;

        if (Random.Range(0f, 100f) <= idleChance)
        {
            animator.ResetTrigger("UnIdle");
            PlayOneShot(idleClip);
            if (float.TryParse(idleClip.length.ToString(), out var result))
                StartCoroutine(IdleWait(result));
            Debug.Log("actual wait time " + result);
        }
    }

    private IEnumerator IdleWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        animancer.Stop(idleClip);
        animator.SetTrigger("UnIdle");
    }

}

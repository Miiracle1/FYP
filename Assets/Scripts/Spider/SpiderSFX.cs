using UnityEngine;

/// <summary>
/// Handles SFX calling for the spider.
/// </summary>
public partial class SpiderAI
{
    [Header("SFX")]
    [SerializeField] private AudioClip idleSFXClip;
    [SerializeField] private AudioClip scaredSFXClip;
    [SerializeField] private AudioClip moveSFXClip;

    /***************************************************************************************************************************************************************************************/
    // SFX Methods

    /// <summary>
    /// Plays scared clip at spider transform.
    /// </summary>
    public void PlayScaredClip()
    {
        if (scaredSFXClip == null) return;

        SFXManager.PlaySfxClip(scaredSFXClip, transform);
    }

}

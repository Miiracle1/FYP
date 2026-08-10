using UnityEngine;

public partial class SpiderAI
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip idleAudioClip;
    [SerializeField] private float idleAudioChance = 40f;

    /***********************************************************************************************************************************************************************************************************************/

    public void RandomIdleAudio(float volume = 1f)
    {
        if (Random.Range(0,100f) < idleAudioChance) return;

        PlayIdleAudioClip(volume);
    }

    private void PlayIdleAudioClip(float volume = 1f)
    {
        audioSource.clip = idleAudioClip;

        audioSource.volume = volume;

        audioSource.Play();
    }
}

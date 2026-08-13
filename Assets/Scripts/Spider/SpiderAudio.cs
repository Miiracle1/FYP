using UnityEngine;

public partial class SpiderAI
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float idleAudioChance = 40f;

    /***********************************************************************************************************************************************************************************************************************/

    public void RandomIdleAudio(float volume = 1f)
    {
        if (audioSource.isPlaying) return;

        if (Random.Range(0,100f) < idleAudioChance) return;

        PlayIdleAudioClip(volume);
    }

    private void PlayIdleAudioClip(float volume = 0.8f)
    {
        var randomInt = Random.Range(1, 4).ToString();

        audioSource.clip = LoadSoundClip(randomInt);

        if (audioSource.clip == null) return;

        audioSource.volume = volume;

        audioSource.Play();
    }

    private AudioClip LoadSoundClip(string clipNum)
    {
        AudioClip idleClip = Resources.Load<AudioClip>("Audio/Spider Idle Audio/Spider Idle " + clipNum);

        return idleClip;
    }
}

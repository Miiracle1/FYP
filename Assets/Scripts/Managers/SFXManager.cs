using UnityEngine;

/// <summary>
/// Mainly controls sfx play logic
/// </summary>
public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
    }

    /// <summary>
    /// Spawns prefab to play an sfx one shot, then it despawns.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="spawnTransform"></param>
    /// <param name="volume"></param>
    public static void PlaySfxClip(AudioClip clip, Transform spawnTransform, float volume = 1f)
    {
        instance.audioSource.clip = clip;

        instance.audioSource.volume = volume;

        instance.audioSource.Play();

        float clipLength = instance.audioSource.clip.length;
    }
}

using UnityEngine;

/// <summary>
/// Mainly controls sfx play logic
/// </summary>
public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] private AudioSource sfxPrefab;

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
    public void PlaySfxClip(AudioClip clip, Transform spawnTransform, float volume)
    {
        var audioSource = Instantiate(sfxPrefab, spawnTransform.position, Quaternion.identity);

        audioSource.clip = clip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}

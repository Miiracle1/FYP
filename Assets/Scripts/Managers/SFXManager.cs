using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Mainly controls sfx play logic
/// </summary>
public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    private AudioSource audioSource;

    [SerializeField] private GameObject audioPrefab;

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

        //float clipLength = instance.audioSource.lobbyStartSFX.length;
    }

    public void PlayClipAtTransform(AudioClip clip, Transform targetTransform, float volume = 1.0f)
    {
        if (clip == null || targetTransform == null) return;

        GameObject dynamicAudioObj = Instantiate(audioPrefab, targetTransform.position, targetTransform.rotation);

        dynamicAudioObj.transform.SetParent(targetTransform);

        AudioSource audioSource = dynamicAudioObj.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;

        //audioSource.outputAudioMixerGroup = SoundMixerManager.instance.GetAudioMixer().FindMatchingGroups("SFX")[0];

        audioSource.Play();

        dynamicAudioObj.SetActive(false);
        Destroy(dynamicAudioObj, clip.length);
    }
}

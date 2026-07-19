using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Controls audio mixer sound volume
/// </summary>
public class SoundMixerManager : MonoBehaviour
{
    public static SoundMixerManager instance;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { 
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
            SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));

        if (PlayerPrefs.HasKey("MusicVolume"))
            SetMasterVolume(PlayerPrefs.GetFloat("MusicVolume"));

        if (PlayerPrefs.HasKey("SfxVolume"))
            SetMasterVolume(PlayerPrefs.GetFloat("SfxVolume"));

        if (PlayerPrefs.HasKey("NarratorVolume"))
            SetMasterVolume(PlayerPrefs.GetFloat("NarratorVolume"));
    }

    /// <summary>
    /// Set master volume to parameter level
    /// </summary>
    /// <param name="volume"></param>
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    /// <summary>
    /// Set music volume to parameter level
    /// </summary>
    /// <param name="volume"></param>
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    /// <summary>
    /// Set sfx volume to parameter level
    /// </summary>
    /// <param name="volume"></param>
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    public void SetNarratorVolume(float volume)
    {
        audioMixer.SetFloat("narratorVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("NarratorVolume", volume);
    }

    public AudioMixer GetAudioMixer()
    { 
        return audioMixer;
    }
}
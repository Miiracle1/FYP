using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Controls audio mixer sound volume
/// </summary>
public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    /// <summary>
    /// Set master volume to parameter level
    /// </summary>
    /// <param name="volume"></param>
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20f);
    }

    /// <summary>
    /// Set music volume to parameter level
    /// </summary>
    /// <param name="volume"></param>
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
    }

    /// <summary>
    /// Set sfx volume to parameter level
    /// </summary>
    /// <param name="volume"></param>
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20f);
    }

    public void SetNarratorVolume(float volume)
    {
        audioMixer.SetFloat("narratorVolume", Mathf.Log10(volume) * 20f);
    }
}
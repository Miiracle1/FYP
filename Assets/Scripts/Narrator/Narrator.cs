using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Handles narrator audio
/// </summary>
[RequireComponent (typeof(AudioSource))]
public class Narrator : MonoBehaviour
{
    public static Narrator instance;

    [Header("Narrator Sound Clips")]
    [SerializeField] private AudioClip clip;

    private AudioSource audioSource;
    private bool isPlaying = false; //flag to control if future want to do smooth fade out if press play again

    private void Awake()
    {
        if (instance == null) 
            instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays narrator audio, should replay if press again and cut of old one immediately
    /// </summary>
    public void PlayNarrator()
    {
        Debug.Log("Narrator Button Pressed");
        if (clip == null) return;

        audioSource.clip = clip;
        audioSource.Play();
    }
}

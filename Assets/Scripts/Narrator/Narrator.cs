using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Handles narrator audio
/// </summary>
[RequireComponent (typeof(AudioSource))]
public class Narrator : MonoBehaviour
{
    [Header("Narrator Sound Clips")]
    [SerializeField] private AudioClip clip;

    private AudioSource audioSource;
    private bool isPlaying = false; //flag to control if future want to do smooth fade out if press play again

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays narrator audio, should replay if press again and cut of old one immediately
    /// </summary>
    public void PlayNarrator(SelectEnterEventArgs args)
    {
        Debug.Log("Narrator Button Pressed");
        if (clip == null) return;

        Vector3 pos = args.interactableObject.transform.position;

        audioSource.clip = clip;
        gameObject.transform.position = pos + new Vector3(0f, 1f, 0f);
        audioSource.Play();
    }
}

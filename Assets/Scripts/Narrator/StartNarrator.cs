using UnityEditor.PackageManager;
using UnityEngine;

/// <summary>
/// Handles voice over narrator
/// </summary>
public class StartNarrator : MonoBehaviour
{
    public static StartNarrator instance;

    [Header("Narrator Sound Clips")]
    [SerializeField] private AudioClip lobbyStartSFX;

    private AudioSource audioSource;
    private bool startFlag = true; //flag to control if future want to do smooth fade out if press play again

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
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
        if (lobbyStartSFX == null || !startFlag) return;
        
        audioSource.clip = lobbyStartSFX;
        audioSource.Play();
        startFlag = false;
    }

    public void PlaySound(NarratorSounds narratorSounds)
    {
        //if (PlayerPrefs.GetString("Tutorial") == "False") return;

        var audioClip = SortAudio(narratorSounds);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private AudioClip SortAudio(NarratorSounds narratorSounds)
    {
        AudioClip currentClip = null;

        switch (narratorSounds)
        {
            case NarratorSounds.garageTutorial:
                currentClip = LoadSoundClip("garage tutorial");
                break;
            case NarratorSounds.garageFoundSpider:
                currentClip = LoadSoundClip("garage found spider");
                break;
            case NarratorSounds.garageCaughtSpider:
                currentClip = LoadSoundClip("garage when spider caught");
                break;
            case NarratorSounds.greenhouseTutorial:
                currentClip = LoadSoundClip("greenhouse welcome");
                break;
            case NarratorSounds.greenhouseEnd:
                currentClip = LoadSoundClip("greenhouse complete");
                break;
            case NarratorSounds.finishLobby:
                currentClip = LoadSoundClip("lobby after green house");
                break;

            default:
                currentClip = LoadSoundClip("garage tutorial");
                break;
        }

        return currentClip;
    }

    private AudioClip LoadSoundClip(string clipName)
    {
        AudioClip explosionClip = Resources.Load<AudioClip>("Audio/" + clipName);

        return explosionClip;
    }
}

public enum NarratorSounds
{
    garageTutorial,
    garageFoundSpider,
    garageCaughtSpider,
    greenhouseTutorial,
    greenhouseEnd,
    finishLobby
}
using UnityEngine;

/// <summary>
/// Something like reseting player prefs, use in inspector only.
/// </summary>
public class DevCheat : MonoBehaviour
{
    public static DevCheat Instance;

    [Header("Cheats")]
    [SerializeField] private bool reset = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (reset)
            ResetPlayerPrefs();
    }

    public bool HardReset
    {
        get => reset;
        set
        {
            if (reset = value) return;
            reset = value;
            ResetPlayerPrefs();
        }
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.SetString("Tutorial", "True");
        PlayerPrefs.SetString("Got Spider", "False");
    }
}

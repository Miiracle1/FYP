using UnityEngine;

/// <summary>
/// Something like reseting player prefs, use in inspector only.
/// </summary>
public class DevCheat : MonoBehaviour
{
    [Header("Cheats")]
    [SerializeField] private bool reset = false;

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

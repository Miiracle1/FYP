using System.Runtime.CompilerServices;
using UnityEngine;

public class BootstrapManager : MonoBehaviour
{
    [SerializeField] private GameObject[] managerPrefabs;

    private SceneEnums sceneEnums;
    private GameModeEnums gamemodeEnums;

    private void Awake()
    {
        InitializeManagers();
    }

    void Start()
    {
        sceneEnums = GameProgressTracker.Scene;

        switch (sceneEnums)
        {
            case SceneEnums.Lobby:
                SceneLoader.instance.LoadScene("Lobby");
                break;

            case SceneEnums.Greenhouse:
                SceneLoader.instance.LoadScene("Greenhouse");
                break;

            case SceneEnums.Garage:
                SceneLoader.instance.LoadScene("Garage");
                break;

            default:
                SceneLoader.instance.LoadScene("Lobby");
                break;
        }
            
    }

    private void InitializeManagers()
    {
        foreach (var manager in managerPrefabs)
        { 
            Instantiate(manager);
        }
    }
}
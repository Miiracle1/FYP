using System.Runtime.CompilerServices;
using UnityEngine;

public class BootstrapManager : MonoBehaviour
{
    [SerializeField] private GameObject[] managerPrefabs;

    private SceneEnums sceneEnums;
    private GameModeEnums gamemodeEnums;

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

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
                Debug.Log("Now laoding lobby yey");
                break;

            case SceneEnums.Greenhouse:
                SceneLoader.instance.LoadScene("GreenHouse");
                Debug.Log("Now laoding greenhouse yey");
                break;

            case SceneEnums.Garage:
                SceneLoader.instance.LoadScene("Garage");
                Debug.Log("Now laoding garage yey");
                break;

            default:
                SceneLoader.instance.LoadScene("Lobby");
                break;
        }
            
    }

    /***************************************************************************************************************************************************************************************/
    //Private Methods

    private void InitializeManagers()
    {
        foreach (var manager in managerPrefabs)
        { 
            Instantiate(manager);
        }
    }
}
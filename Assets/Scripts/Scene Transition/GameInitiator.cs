using System.Collections;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject xrRig;
    [SerializeField] private GameObject secondaryToLoad;
    [SerializeField] private GameObject thirdToLoad;
    [SerializeField] private GameObject fourthToLoad;
    [SerializeField] private GameObject fifthToLoad;

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    private IEnumerator Start()
    {
        yield return StartCoroutine(InitializeXR());

        yield return StartCoroutine(InitializeSecondary());

        yield return StartCoroutine(InitialzeThird());

        yield return StartCoroutine(InitialzeFourth());

        yield return StartCoroutine(InitialzeFifth());

        GameInitializeComplete();

        yield return null;
    }

    private IEnumerator InitializeXR()
    { 
        xrRig = Instantiate(xrRig);
        yield return null;
    }

    private IEnumerator InitializeSecondary()
    {
        if (secondaryToLoad != null)
        { 
            secondaryToLoad = Instantiate(secondaryToLoad);
        }

        yield return null;
    }

    private IEnumerator InitialzeThird()
    {
        if (thirdToLoad != null) 
        {
            thirdToLoad = Instantiate(thirdToLoad);
        } 

        yield return null;
            
    }

    private IEnumerator InitialzeFourth()
    {
        if (fourthToLoad != null)
        { 
            fourthToLoad = Instantiate(fourthToLoad);
        }

        yield return null;
    }

    private IEnumerator InitialzeFifth()
    { 
        if (fifthToLoad != null)
        {
            fifthToLoad = Instantiate(fifthToLoad);
        }

        yield return null;
    }

    private void GameInitializeComplete()
    {
        FadeCanvas fadeCanvas = FindFirstObjectByType<FadeCanvas>();

        if (fadeCanvas != null)
        {
            fadeCanvas.FadeOut();
        }
        else
            Debug.Log("Game Initiator couldn't find Fade Canvas or something went wrong");
    }    
}

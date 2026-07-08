using System.Collections;
using UnityEngine;

/// <summary>
/// Handles Indicator Logic.
/// Attach on Indicator prefab.
/// </summary>
public class Indicator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float selfDestructTimer = 3f;

    private Coroutine coroutine;
    private ParticleSystem particle;

    /***************************************************************************************************************************************************************************************/

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        if (coroutine != null)
        {
            particle.Play();
            StartCoroutine(IndicatorRoutine());
        }
    }

    private IEnumerator IndicatorRoutine()
    { 
        yield return new WaitForSeconds(selfDestructTimer);

        gameObject.SetActive(false);

        Destroy(gameObject);
    }
}

using System.Collections;
using UnityEngine;

/// <summary>
/// Fades canvas while transitioning scenes
/// </summary>
[RequireComponent (typeof(CanvasGroup))]
public class FadeCanvas : MonoBehaviour
{
    [SerializeField] private float defaultDuration = 1.0f;
    [SerializeField] private bool startVisible = false;

    [SerializeField] private CanvasGroup canvasGroup;
    private Coroutine currentCoroutine;

    private void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = startVisible ? 1.0f : 0.0f;
    }

    [ContextMenu("Test Fade In")]
    public void FadeIn()
    { 
        FadeIn(defaultDuration);
    }

    public void FadeIn(float duration)
    {
        if (currentCoroutine != null)
        { 
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(FadeCoroutine(1f, duration));
    }

    [ContextMenu("Test Fade Out")]
    public void FadeOut()
    {
        FadeOut(defaultDuration);
    }

    public void FadeOut(float duration)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(FadeCoroutine(0f, duration));
    }

    private IEnumerator FadeCoroutine(float targetAlpha, float duration)
    { 
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        if (targetAlpha == 1f) canvasGroup.blocksRaycasts = true;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time/duration);
            time += Time.deltaTime;
            yield return null;
        }

        if (targetAlpha == 0f) canvasGroup.blocksRaycasts = false;

        canvasGroup.alpha = targetAlpha;
        currentCoroutine = null;
    }
}

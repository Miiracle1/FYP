using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Handles player Health and Death logic
/// </summary>
public partial class Player
{
    [Header("Player Health")]
    [SerializeField] private float health;

    [Header("Respawn Settings")]
    [SerializeField] private float respawnTime = 5.0f;

    private float currentHealth;
    private bool isDeath = false;
    private bool isRespawning = false;

    public static event Action<float> OnPlayerHeathChanged;
    public static event Action OnPlayerDeath;

    /// <summary>
    /// Initialize player health, put in Start
    /// </summary>
    private void InitializeHealth()
    { 
        currentHealth = health;
        isDeath = false;
    }

    public void RestoreHeath(float heal)
    {
        if (currentHealth >= 0.1f)
        {
            currentHealth += heal;
            OnPlayerHeathChanged?.Invoke(currentHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth >= 0.1f)
        { 
            currentHealth -= damage;
            OnPlayerHeathChanged?.Invoke(currentHealth);
        }

        if (currentHealth <= 0f && !isDeath)
        {
            //Death
            OnPlayerDeath?.Invoke();
            Death();
        }
    }

    public void Death()
    { 
        isDeath = true;

        GameProgressTracker.PlayerState = PlayerStateEnums.Dead;

        FadeCanvas fadeCanvas = FindFirstObjectByType<FadeCanvas>();

        if (fadeCanvas != null)
        {
            fadeCanvas.FadeIn();

            if (!isRespawning) 
                StartCoroutine(WaitRespawnCoroutine());
        }
        else
        { 
            Debug.LogError("Fade Canvas not found");
            return;
        }
    }

    private IEnumerator WaitRespawnCoroutine()
    {
        isRespawning = true;

        GameProgressTracker.PlayerState = PlayerStateEnums.Respawning;

        yield return new WaitForSeconds(respawnTime);

        Respawn();

    }

    private void Respawn()
    {
        isDeath = false;

        gameObject.transform.position = FindFirstObjectByType<RespawnPoint>().transform.position;
        FadeCanvas fadeCanvas = FindFirstObjectByType<FadeCanvas>();

        if (fadeCanvas != null)
        {
            fadeCanvas.FadeOut();
        }
        else
        {
            Debug.LogError("Fade Canvas not found");
            return;
        }
        isRespawning = false;

        GameProgressTracker.PlayerState = PlayerStateEnums.Alive;
    }
}

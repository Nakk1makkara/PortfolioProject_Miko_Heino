using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public HealthBar healthBar;
    public AudioSource hitSound;
    public GameObject deathScreen;
    private AudioManager audioManager;

    void Start()
    {
        health = maxHealth;

        audioManager = FindObjectOfType<AudioManager>();

        if (healthBar == null)
        {
            Debug.LogError("Health bar reference not set!");
        }

        if (deathScreen == null)
        {
            Debug.LogError("Death screen reference not set!");
        }
        else
        {
            deathScreen.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (hitSound != null)
        {
            hitSound.Play();
        }

        UpdateHealthBar();

        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    void Die()
    {
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
        }

        if (audioManager != null)
        {
            audioManager.PlayDeathMusic();
        }


        Destroy(gameObject);
    }

}

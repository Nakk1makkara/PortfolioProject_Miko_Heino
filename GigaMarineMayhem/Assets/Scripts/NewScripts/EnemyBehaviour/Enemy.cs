using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    public AudioSource hitSound; 

    // Reference to the progress bar
    private ProgressBar progressBar;

    private void Start()
    {
        // Find the ProgressBar script in the scene
        progressBar = FindObjectOfType<ProgressBar>();

        if (progressBar == null)
        {
            Debug.LogError("ProgressBar script not found in the scene.");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            PlayHitSound(); // Play sound when taking damage
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

        // Notify the progress bar that an enemy has been killed
        if (progressBar != null)
        {
            progressBar.GoalAchieved();
        }
    }

    void PlayHitSound()
    {
        if (hitSound != null)
        {
            hitSound.Play();
        }
    }
}

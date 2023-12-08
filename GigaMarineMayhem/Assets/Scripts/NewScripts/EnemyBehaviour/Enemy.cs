using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    public AudioSource hitSound; 

    private ProgressBar progressBar;

    private void Start()
    {
        
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
            PlayHitSound(); 
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

        
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

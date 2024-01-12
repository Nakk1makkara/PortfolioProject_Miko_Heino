using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    public AudioSource hitSound;

    public GameObject[] powerUps;
    public float dropChance = 0.2f;

    private ProgressBar progressBar;
    private bool hasAchievedGoal = false;

    void Start()
    {
        progressBar = FindObjectOfType<ProgressBar>();

        if (progressBar == null)
        {
            Debug.LogError("ProgressBar script not found");
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
        if (!hasAchievedGoal)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            SpawnPowerUp();

            Destroy(gameObject);

            if (progressBar != null)
            {
                progressBar.GoalAchieved();
                hasAchievedGoal = true;
            }
        }
    }

    void PlayHitSound()
    {
        if (hitSound != null)
        {
            hitSound.Play();
        }
    }

    void SpawnPowerUp()
    {
        if (Random.value < dropChance)
        {
            GameObject powerUpPrefab = powerUps[Random.Range(0, powerUps.Length)];

            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        }
    }
}

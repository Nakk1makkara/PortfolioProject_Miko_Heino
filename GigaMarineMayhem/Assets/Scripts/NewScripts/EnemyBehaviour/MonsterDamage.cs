using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int damage;
    public float damageInterval = 1.0f; // Adjust the interval as needed
    public PlayerHealth playerHealth;
    private bool isDealingDamage = false;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component not found in the scene.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Start dealing continuous damage
            isDealingDamage = true;
            StartCoroutine(DealContinuousDamage());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Stop dealing continuous damage when the player exits the collision
            isDealingDamage = false;
        }
    }

    private IEnumerator DealContinuousDamage()
    {
        while (isDealingDamage)
        {
            // Deal damage and wait for the specified interval
            playerHealth.TakeDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}

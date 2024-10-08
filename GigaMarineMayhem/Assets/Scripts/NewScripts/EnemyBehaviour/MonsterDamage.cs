using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int damage;
    public float damageInterval = 1.0f; 
    public PlayerHealth playerHealth;
    private bool isDealingDamage = false;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component not found");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            isDealingDamage = true;
            StartCoroutine(DealContinuousDamage());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            isDealingDamage = false;
        }
    }

    private IEnumerator DealContinuousDamage()
    {
        while (isDealingDamage)
        {
            
            playerHealth.TakeDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}

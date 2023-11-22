using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public HealthBar healthBar;

    void Start()
    {
        health = maxHealth;

       
        if (healthBar == null)
        {
            Debug.LogError("Health bar reference not set!");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        
        UpdateHealthBar();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateHealthBar()
    {
       
        healthBar.UpdateHealthBar(health, maxHealth);
    }
}

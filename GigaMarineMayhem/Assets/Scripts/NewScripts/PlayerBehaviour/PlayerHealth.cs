using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    void Start()
    {
        health = maxHealth;
    }

    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

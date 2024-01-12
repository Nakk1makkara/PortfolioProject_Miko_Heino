using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int numExplosions = 8;
    public float explosionForce = 10f;
    public float explosionRadius = 5f;
    public GameObject bulletPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;

            SpawnBullets();
            Destroy(gameObject, 0.1f); 
        }
    }

    void SpawnBullets()
    {
        float angleIncrement = 360f / numExplosions;

        for (int i = 0; i < numExplosions; i++)
        {
            float angle = i * angleIncrement;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * explosionForce, ForceMode2D.Impulse);
        }
    }
}

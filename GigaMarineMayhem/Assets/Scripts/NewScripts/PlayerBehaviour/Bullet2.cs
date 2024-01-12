using UnityEngine;
using System.Collections;

public class Bullet2 : MonoBehaviour
{
    public GameObject hitEffect;
    public int damage = 10;

    private bool hasHit = false;

    void Start()
    {
        StartCoroutine(DestroyAfterDelay(10f));
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                hasHit = true;
            }
        }

        
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!hasHit)
        {
            Destroy(gameObject);
        }
    }
}

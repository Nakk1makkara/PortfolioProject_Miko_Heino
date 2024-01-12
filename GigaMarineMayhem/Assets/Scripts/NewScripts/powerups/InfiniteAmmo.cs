using System.Collections;
using UnityEngine;

public class InfiniteAmmo : MonoBehaviour
{
    public float duration = 10f;

    private bool isPowerUpActive = false;
    private SpriteRenderer powerUpRenderer;

    private void Start()
    {
        powerUpRenderer = GetComponent<SpriteRenderer>();

        if (powerUpRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPowerUpActive)
        {
            ApplyInfiniteAmmoPowerUp(other.gameObject);
        }
    }

    private void ApplyInfiniteAmmoPowerUp(GameObject player)
    {
        Shooting shooting = player.GetComponent<Shooting>();

        if (shooting != null)
        {
            StartCoroutine(ActivateInfiniteAmmoPowerUp(shooting));
        }
    }

    private IEnumerator ActivateInfiniteAmmoPowerUp(Shooting shooting)
    {
        isPowerUpActive = true;

        if (powerUpRenderer != null)
        {
            powerUpRenderer.enabled = false;
        }

        shooting.EnableInfiniteAmmo();

        Debug.Log("Infinite Ammo applied");

        yield return new WaitForSeconds(duration);

        shooting.DisableInfiniteAmmo();

        Debug.Log("Infinite Ammo duration ended");

        isPowerUpActive = false;

        if (powerUpRenderer != null)
        {
            powerUpRenderer.enabled = true;
        }

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeed : MonoBehaviour
{
    public float speedMultiplier = 2f;
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
            ApplySpeedPowerUp(other.gameObject);
        }
    }

    private void ApplySpeedPowerUp(GameObject player)
    {
        PlayerMovements playerMovements = player.GetComponent<PlayerMovements>();

        if (playerMovements != null)
        {
            StartCoroutine(ActivateSpeedPowerUp(playerMovements));
        }
    }

    private IEnumerator ActivateSpeedPowerUp(PlayerMovements playerMovements)
    {
        isPowerUpActive = true;

        if (powerUpRenderer != null)
        {
            powerUpRenderer.enabled = false;
        }

        float originalMoveSpeed = playerMovements.moveSpeed;

        playerMovements.moveSpeed *= speedMultiplier;

        Debug.Log("power-up applied");

        yield return new WaitForSeconds(duration);

        playerMovements.moveSpeed = originalMoveSpeed;

        Debug.Log("duration ended");

        isPowerUpActive = false;

        if (powerUpRenderer != null)
        {
            powerUpRenderer.enabled = true;
        }

        Destroy(gameObject);
    }
}

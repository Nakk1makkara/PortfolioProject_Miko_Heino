// EnemySpawner.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject swarmerPrefab;
    [SerializeField]
    private float swarmerInterval = 3.5f;

    // Reference to the player
    private GameObject player;

    void Start()
    {
        // Find the player GameObject in the scene
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found in the scene. Make sure the player has the 'Player' tag.");
        }

        StartCoroutine(spawnEnemy(swarmerInterval, swarmerPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        while (true) // Infinite loop to keep spawning enemies
        {
            yield return new WaitForSeconds(interval);

            // Instantiate the enemy
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6, 6f), 0), Quaternion.identity);

            // Pass the player reference to the MonsterDamage script
            MonsterDamage monsterDamage = newEnemy.GetComponent<MonsterDamage>();
            if (monsterDamage != null)
            {
                monsterDamage.playerHealth = player.GetComponent<PlayerHealth>();
            }

            // Pass the player reference to the AIchase script
            AIchase aiChase = newEnemy.GetComponent<AIchase>();
            if (aiChase != null)
            {
                aiChase.player = player;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject swarmerPrefab;
    [SerializeField]
    private float swarmerInterval = 3.5f;
    private GameObject player;
    public ProgressBar progressBar;
    private bool isLevelCompleted = false;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found in the scene. Make sure the player has the 'Player' tag.");
        }

       
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true) 
        {
            
            if (progressBar != null && progressBar.IsLevelComplete())
            {
                isLevelCompleted = true;
                break; 
            }

            yield return new WaitForSeconds(swarmerInterval);

            
            GameObject newEnemy = Instantiate(swarmerPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(-6, 6f), 0), Quaternion.identity);

            
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

        // Destroy any remaining enemies
        DestroyRemainingEnemies();
    }

    private void DestroyRemainingEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}

using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject swarmerPrefab;
    [SerializeField] private GameObject otherEnemyPrefab;
    [SerializeField] private float swarmerSpawnRate = 3.5f;
    [SerializeField] private float otherEnemySpawnRate = 5f;
    private GameObject player;
    public ProgressBar progressBar;
    private bool isLevelCompleted = false;

    [SerializeField] private Transform spawnPoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found in the scene");
        }

        if (spawnPoint == null)
        {
            spawnPoint = transform;
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

            float randomX = Random.Range(-5f, 5f);
            float randomY = Random.Range(-6f, 6f);
            Vector3 spawnPosition = spawnPoint.position + new Vector3(randomX, randomY, 0);

            yield return new WaitForSeconds(RandomSpawnInterval());

            GameObject newEnemy;
            float randomEnemy = Random.value;

            if (randomEnemy < swarmerSpawnRate / (swarmerSpawnRate + otherEnemySpawnRate))
            {
                newEnemy = Instantiate(swarmerPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                newEnemy = Instantiate(otherEnemyPrefab, spawnPosition, Quaternion.identity);
            }

            AttachComponentsToEnemy(newEnemy);
        }

        DestroyRemainingEnemies();
    }

    private float RandomSpawnInterval()
    {
        return Random.Range(swarmerSpawnRate, swarmerSpawnRate + otherEnemySpawnRate);
    }

    private void AttachComponentsToEnemy(GameObject enemy)
    {
        MonsterDamage monsterDamage = enemy.GetComponent<MonsterDamage>();
        if (monsterDamage != null)
        {
            monsterDamage.playerHealth = player.GetComponent<PlayerHealth>();
        }

        AIchase aiChase = enemy.GetComponent<AIchase>();
        if (aiChase != null)
        {
            aiChase.player = player;
        }
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

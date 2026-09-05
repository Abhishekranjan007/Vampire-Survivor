using UnityEngine;

public class EnemySpawner : MonoBehaviour
{    

    [Header("References")]    
    [SerializeField] private Transform player;
    [SerializeField] private EnemyPool enemyPool;

    [Header("Spawn Settings")]
    //[SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private float spawnDistance = 20f;

    private EnemyFactory enemyFactory;
    private float spawnTimer;
    private WaveData currentWave;


    private void Awake()
    {
        enemyFactory = new EnemyFactory(enemyPool);
    }

    public void SetWave(WaveData wave)
    {
        currentWave = wave;
        spawnTimer = 0f;
    }

    private void Update()
    {
        if (currentWave == null)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= currentWave.spawnInterval)
        {
            spawnTimer -= currentWave.spawnInterval;
            if (enemyPool.ActiveEnemyCount < currentWave.maxActiveEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        EnemyData enemyData = GetRandomEnemy();

        if (enemyData == null)
            return;

        Vector2 direction = Random.insideUnitCircle.normalized;

        Vector3 spawnPosition = player.position;

        spawnPosition.x += direction.x * spawnDistance;
        spawnPosition.z += direction.y * spawnDistance;
        spawnPosition.y = 1f;

        enemyFactory.Create(
            enemyData,
            spawnPosition,
            player
        );
    }

    private EnemyData GetRandomEnemy()
    {
        if (currentWave.enemies == null ||
            currentWave.enemies.Length == 0)
        {
            return null;
        }

        float totalWeight = 0f;

        for (int i = 0; i < currentWave.enemies.Length; i++)
        {
            totalWeight += currentWave.enemies[i].spawnWeight;
        }

        if (totalWeight <= 0f)
            return null;

        float randomValue = Random.Range(0f, totalWeight);

        for (int i = 0; i < currentWave.enemies.Length; i++)
        {
            randomValue -= currentWave.enemies[i].spawnWeight;

            if (randomValue <= 0f)
            {
                return currentWave.enemies[i].enemy;
            }
        }

        return currentWave.enemies[^1].enemy;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyTestSpawner : MonoBehaviour
{
    
    [SerializeField] private EnemyData enemyData;    
    [SerializeField] private Transform player;
    [SerializeField] private EnemyPool enemyPool;

    private EnemyFactory enemyFactory;
    private Enemy spawnedEnemy;

    private void Awake()
    {
        enemyFactory = new EnemyFactory(enemyPool);
    }

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        spawnedEnemy = enemyFactory.Create(
            enemyData,
            new Vector3(5f, 1f, 5f),
            player
        );
    }

    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        if (spawnedEnemy == null)
            return;

        spawnedEnemy.TakeDamage(9999f);

        SpawnEnemy();
    }
}
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public int ActiveEnemyCount { get; private set; }

    private readonly Dictionary<EnemyData, Queue<Enemy>> pools = new();

    public Enemy Get(EnemyData data, Transform player)
    {
        if (!pools.TryGetValue(data, out Queue<Enemy> pool))
        {
            pool = new Queue<Enemy>();
            pools.Add(data, pool);
        }

        Enemy enemy;

        if (pool.Count > 0)
        {
            enemy = pool.Dequeue();
        }
        else
        {
            enemy = CreateNewEnemy(data);
        }

        enemy.gameObject.SetActive(true);
        enemy.Initialize(player);

        ActiveEnemyCount++;

        return enemy;
    }

    public void Release(Enemy enemy)
    {
        EnemyData data = enemy.Data;

        enemy.ResetEnemy();
        enemy.gameObject.SetActive(false);

        ActiveEnemyCount = Mathf.Max(0, ActiveEnemyCount - 1);

        if (!pools.TryGetValue(data, out Queue<Enemy> pool))
        {
            pool = new Queue<Enemy>();
            pools.Add(data, pool);
        }

        pool.Enqueue(enemy);
    }

    private Enemy CreateNewEnemy(EnemyData data)
    {
        GameObject enemyObject = Instantiate(data.prefab, transform);

        Enemy enemy = enemyObject.GetComponent<Enemy>();

        if (enemy == null)
        {
            Debug.LogError(
                $"Enemy prefab '{data.prefab.name}' does not contain an Enemy component."
            );

            Destroy(enemyObject);
            return null;
        }

        enemy.SetPool(this);

        enemyObject.SetActive(false);

        return enemy;
    }
}
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private readonly EnemyPool pool;

    public EnemyFactory(EnemyPool enemyPool)
    {
        pool = enemyPool;
    }

    public Enemy Create(EnemyData data, Vector3 position, Transform player)
    {
        Enemy enemy = pool.Get(data, player);

        enemy.transform.position = position;

        return enemy;
    }
}

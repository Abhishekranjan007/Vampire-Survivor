using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private int initialSize = 30;

    private readonly Dictionary<GameObject, Queue<Projectile>> pools =
        new Dictionary<GameObject, Queue<Projectile>>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < projectilePrefabs.Length; i++)
        {
            CreatePool(projectilePrefabs[i]);
        }
    }

    private void CreatePool(GameObject prefab)
    {
        if (prefab == null)
            return;

        if (pools.ContainsKey(prefab))
            return;

        Queue<Projectile> pool = new Queue<Projectile>();

        pools.Add(prefab, pool);

        for (int i = 0; i < initialSize; i++)
        {
            CreateProjectile(prefab, pool);
        }
    }

    private Projectile CreateProjectile(
        GameObject prefab,
        Queue<Projectile> pool)
    {
        GameObject obj = Instantiate(prefab, transform);

        obj.SetActive(false);

        Projectile projectile =
            obj.GetComponent<Projectile>();

        projectile.SetPool(this, prefab);

        pool.Enqueue(projectile);

        return projectile;
    }

    public Projectile Get(GameObject prefab)
    {
        if (prefab == null)
            return null;

        if (!pools.ContainsKey(prefab))
        {
            CreatePool(prefab);
        }

        Queue<Projectile> pool = pools[prefab];

        if (pool.Count == 0)
        {
            CreateProjectile(prefab, pool);
        }

        Projectile projectile = pool.Dequeue();

        projectile.gameObject.SetActive(true);

        return projectile;
    }

    public void Return(
        Projectile projectile,
        GameObject prefab)
    {
        if (projectile == null || prefab == null)
            return;

        projectile.gameObject.SetActive(false);

        if (!pools.ContainsKey(prefab))
        {
            pools.Add(
                prefab,
                new Queue<Projectile>()
            );
        }

        pools[prefab].Enqueue(projectile);
    }
}
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private float lifetime;
    private float timer;

    private ProjectilePool pool;
    private GameObject poolPrefab;

    public void Initialize(float projectileDamage, float speed, float projectileLifetime)
    {
        damage = projectileDamage;
        lifetime = projectileLifetime;
        timer = 0f;

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speed;
        }
    }

    private void OnEnable()
    {
        pool = GetComponentInParent<ProjectilePool>();
    }

    public void SetPool(ProjectilePool projectilePool, GameObject prefab)
    {
        pool = projectilePool;
        poolPrefab = prefab;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            //gameObject.SetActive(false);
            ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"PROJECTILE HIT: {other.name}");

        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy == null)
        {
            //Debug.Log("Hit something, but it is not an Enemy.");
            return;
        }

        //Debug.Log("PROJECTILE HIT ENEMY");

        enemy.TakeDamage(damage);

        gameObject.SetActive(false);
    }

    private void ReturnToPool()
    {
        if (pool != null)
        {
            pool.Return(this, poolPrefab);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

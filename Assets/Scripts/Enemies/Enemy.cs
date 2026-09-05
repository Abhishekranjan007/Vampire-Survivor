using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    
    private XPPickupPool xpPickupPool;

    private EnemyPool pool;
    private Transform player;
    private float currentHealth;

    [SerializeField] private float contactDamage = 10f;
    [SerializeField] private float contactDamageInterval = 1f;

    private float contactDamageTimer;

    public EnemyData Data => data;

    private void Awake()
    {
        currentHealth = data.maxHealth;
        xpPickupPool = FindFirstObjectByType<XPPickupPool>();
    }

    private void OnEnable()
    {
        if (EnemyRegistry.Instance != null)
            EnemyRegistry.Instance.Register(this);
    }

    private void OnDisable()
    {
        if (EnemyRegistry.Instance != null)
            EnemyRegistry.Instance.Unregister(this);
    }

    public void SetPool(EnemyPool enemyPool)
    {
        pool = enemyPool;
    }

    public void Initialize(Transform target)
    {
        player = target;
        currentHealth = data.maxHealth;
    }

    private void Update()
    {
        if (player == null)
            return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            direction.Normalize();

            transform.position +=
                direction * data.moveSpeed * Time.deltaTime;
        }

        // Contact damage
        contactDamageTimer -= Time.deltaTime;

        if (contactDamageTimer <= 0f)
        {
            float distance =
                Vector3.Distance(transform.position, player.position);

            if (distance <= 1.2f)
            {
                PlayerHealth playerHealth =
                    player.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(contactDamage);

                    Debug.Log(
                        $"Enemy damaged player for {contactDamage}"
                    );

                    contactDamageTimer =
                        contactDamageInterval;
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        //Debug.Log($"{name} took {damage} damage. Health: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        SpawnXP();

        if (pool != null)
        {
            pool.Release(this);
        }
    }

    private void SpawnXP()
    {
        if (xpPickupPool == null)
            return;

        xpPickupPool.Get(
            transform.position,
            data.xpReward
        );
    }

    
    public void ResetEnemy()
    {
        player = null;
        currentHealth = data.maxHealth;
        contactDamageTimer = 0f;
    }

    
}
using UnityEngine;
using System.Collections.Generic;

public class Weapon
{
    private WeaponData data;

    private float damage;
    private float attackInterval;
    private float projectileSpeed;
    private float attackTimer;

    public Weapon(WeaponData weaponData)
    {
        data = weaponData;

        damage = data.damage;
        attackInterval = data.attackInterval;
        projectileSpeed = data.projectileSpeed;
    }

    public void Update(Transform player)
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            attackTimer -= attackInterval;
            Attack(player);
        }
    }

    private void Attack(Transform player)
    {
        Enemy target = FindClosestEnemy(player);

        if (target == null)
            return;

        Vector3 direction =
            target.transform.position - player.position;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Vector3 spawnPosition =
            player.position + Vector3.up * 0.5f;

        if (ProjectilePool.Instance == null)
            return;

        Projectile projectile =
            ProjectilePool.Instance.Get(data.projectilePrefab);

        if (projectile == null)
            return;

        projectile.transform.SetPositionAndRotation(
            spawnPosition,
            Quaternion.LookRotation(direction)
        );

        //Projectile projectileComponent =
        //    projectile.GetComponent<Projectile>();

        projectile.Initialize(
            damage,
            projectileSpeed,
            data.lifetime
        );
    }

    private Enemy FindClosestEnemy(Transform player)
    {
        if (EnemyRegistry.Instance == null)
            return null;

        IReadOnlyList<Enemy> enemies =
            EnemyRegistry.Instance.GetEnemies();

        Enemy closest = null;

        float maxRangeSqr =
            data.attackRange * data.attackRange;

        const float minimumTargetDistance = 1f;

        float closestDistance = maxRangeSqr;

        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy enemy = enemies[i];

            if (enemy == null)
                continue;

            Vector3 offset =
                enemy.transform.position - player.position;

            offset.y = 0f;

            float distance = offset.sqrMagnitude;

            if (distance <
                minimumTargetDistance * minimumTargetDistance)
                continue;

            if (distance >= closestDistance)
                continue;

            closestDistance = distance;
            closest = enemy;
        }

        return closest;
    }

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeType.Damage:
                damage += upgrade.value;
                break;

            case UpgradeType.AttackSpeed:
                attackInterval *= (1f - upgrade.value);
                break;

            case UpgradeType.ProjectileSpeed:
                projectileSpeed *= (1f + upgrade.value);
                break;
        }
    }
}

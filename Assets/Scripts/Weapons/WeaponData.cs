using UnityEngine;

[CreateAssetMenu(menuName = "Wave Survival/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;

    [Header("Combat")]
    public float damage = 20f;
    public float attackInterval = 1f;
    public float projectileSpeed = 10f;
    public float lifetime = 3f;

    [Header("Targeting")]
    public float attackRange = 15f;

    [Header("Projectile")]
    public GameObject projectilePrefab;    
}

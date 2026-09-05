using UnityEngine;

[CreateAssetMenu(menuName = "Wave Survival/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Identity")]
    public string enemyName;

    [Header("Stats")]
    public float maxHealth = 100f;
    public float moveSpeed = 2f;
    public int xpReward = 10;
    public int contactDamage = 10;

    [Header("Visuals")]
    public GameObject prefab;
}

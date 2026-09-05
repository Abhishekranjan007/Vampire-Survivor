using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Survival/Wave Data")]
public class WaveData : ScriptableObject
{
    [Serializable]
    public class EnemySpawnData
    {
        public EnemyData enemy;
        public float spawnWeight = 1f;
    }

    [Header("Wave Settings")]
    public float duration = 60f;
    public float spawnInterval = 1.5f;
    public int maxActiveEnemies = 30;


    [Header("Enemies")]
    public EnemySpawnData[] enemies;
}

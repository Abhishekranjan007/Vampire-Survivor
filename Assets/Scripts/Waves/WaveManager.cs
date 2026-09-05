using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public enum WaveState
    {
        Starting,
        Active,
        Complete
    }

    [Header("Wave Configuration")]
    [SerializeField] private WaveData[] waves;
    [SerializeField] private EnemySpawner enemySpawner;

    private int currentWaveIndex = -1;
    private int currentWaveNumber = 0;
    private float waveTimer;

    public WaveState CurrentState { get; private set; }
    public int CurrentWaveNumber => currentWaveNumber;
    public float WaveTimeRemaining => Mathf.Max(0f, waveTimer);    
    

    private void Start()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (CurrentState != WaveState.Active)
            return;

        waveTimer -= Time.deltaTime;

        if (waveTimer <= 0f)
        {
            CompleteCurrentWave();
        }
    }

    private void StartNextWave()
    {
        currentWaveIndex++;
        currentWaveNumber++;

        if (currentWaveIndex >= waves.Length)
        {
            currentWaveIndex = 0;
        }

        WaveData wave = waves[currentWaveIndex];

        waveTimer = wave.duration;

        CurrentState = WaveState.Starting;

        enemySpawner.SetWave(wave);

        CurrentState = WaveState.Active;

        // Debug.Log($"Starting Wave {CurrentWaveNumber}");
    }

    private void CompleteCurrentWave()
    {
        //Debug.Log($"Wave {CurrentWaveNumber} Complete!");

        StartNextWave();
    }
}
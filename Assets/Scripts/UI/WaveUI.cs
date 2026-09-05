using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text timerText;

    private void Update()
    {
        int seconds =
            Mathf.CeilToInt(waveManager.WaveTimeRemaining);

        waveText.text =
            $"Wave {waveManager.CurrentWaveNumber}";

        timerText.text =
            $"{seconds / 60:00}:{seconds % 60:00}";
    }
}
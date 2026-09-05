using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateBar;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateBar;
    }

    private void Start()
    {
        UpdateBar(
            playerHealth.CurrentHealth,
            100f
        );
    }

    private void UpdateBar(float current, float maximum)
    {
        if (maximum <= 0f)
        {
            slider.value = 0f;
            return;
        }

        slider.value = current / maximum;
    }
}
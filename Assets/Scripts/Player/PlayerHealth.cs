using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;

    public float CurrentHealth { get; private set; }

    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0f || CurrentHealth <= 0f)
            return;

        CurrentHealth -= amount;

        if (CurrentHealth < 0f)
            CurrentHealth = 0f;

        OnHealthChanged?.Invoke(
            CurrentHealth,
            maxHealth
        );

        if (CurrentHealth <= 0f)
        {
            OnDeath?.Invoke();
        }
    }
}
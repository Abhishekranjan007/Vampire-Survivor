using UnityEngine;
using System;

public class XPSystem : MonoBehaviour
{
    public event Action<float, float> OnXPChanged;
    public event Action<int> OnLevelUp;

    public int Level { get; private set; } = 1;
    public float CurrentXP { get; private set; }
    public float XPToNextLevel { get; private set; } = 30f;

    //private void Awake()
    //{
    //    OnLevelUp += HandleLevelUp;
    //}

    //private void OnDestroy()
    //{
    //    OnLevelUp -= HandleLevelUp;
    //}

    //private void HandleLevelUp(int newLevel)
    //{
    //    Debug.Log($"LEVEL UP! New Level: {newLevel}");
    //}

    public void AddXP(float amount)
    {
        if (amount <= 0f)
            return;

        CurrentXP += amount;

        while (CurrentXP >= XPToNextLevel)
        {
            CurrentXP -= XPToNextLevel;

            Level++;

            XPToNextLevel = CalculateXPRequired(Level);

            OnLevelUp?.Invoke(Level);
        }

        OnXPChanged?.Invoke(CurrentXP, XPToNextLevel);
        //Debug.Log($"XP: {CurrentXP}/{XPToNextLevel} | Level: {Level}");
    }

    private float CalculateXPRequired(int level)
    {
        return 100f + (level - 1) * 50f;
    }
}

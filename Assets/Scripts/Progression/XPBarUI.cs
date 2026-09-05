using UnityEngine;
using UnityEngine.UI;

public class XPBarUI : MonoBehaviour
{
    [SerializeField] private XPSystem xpSystem;
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        xpSystem.OnXPChanged += UpdateBar;
    }

    private void OnDisable()
    {
        xpSystem.OnXPChanged -= UpdateBar;
    }

    private void Start()
    {
        UpdateBar(
            xpSystem.CurrentXP,
            xpSystem.XPToNextLevel
        );
    }

    private void UpdateBar(float currentXP, float requiredXP)
    {
        if (requiredXP <= 0f)
        {
            slider.value = 0f;
            return;
        }

        slider.value = currentXP / requiredXP;
    }
}
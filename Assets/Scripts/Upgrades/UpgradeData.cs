using UnityEngine;

public enum UpgradeType
{
    Damage,
    AttackSpeed,
    ProjectileSpeed
}

[CreateAssetMenu(menuName = "Wave Survival/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;

    public UpgradeType upgradeType;
    public float value;
}

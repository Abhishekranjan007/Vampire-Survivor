using UnityEngine;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponData[] startingWeapons;

    private readonly List<Weapon> weapons = new List<Weapon>();

    private void Awake()
    {
        for (int i = 0; i < startingWeapons.Length; i++)
        {
            weapons.Add(new Weapon(startingWeapons[i]));
        }
    }

    private void Update()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].Update(transform);
        }
    }

    public void AddWeapon(WeaponData weaponData)
    {
        if (weaponData == null)
            return;

        weapons.Add(new Weapon(weaponData));
    }

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        if (weapons.Count == 0)
            return;

        // For now, apply the upgrade to the first weapon.
        weapons[0].ApplyUpgrade(upgrade);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObectsContainer;
    [SerializeField] WeaponData startingWeapon;
    Level level;

    List<WeaponBase> weapons;


    private void Awake()
    {
        weapons = new List<WeaponBase>();
        level = GetComponent<Level>();
    }


    private void Start()
    {
        if (startingWeapon != null)
        {
            AddWeapon(startingWeapon);
        }
    }

    public void AddWeapon(WeaponData weaponData)
    {
        weaponData.currentLevel = 1;
        GameObject weaponGameObject = Instantiate(weaponData.weaponBasePrefab, weaponObectsContainer);

        WeaponBase weaponBase = weaponGameObject.GetComponent<WeaponBase>();

        weaponBase.SetData(weaponData);
        weapons.Add(weaponBase);

        if (level != null)
        {
            level.AddUpgradesIntoTheListOfAvailableUpgrades(weaponData.upgrades[weaponData.currentLevel - 1]);
        }
    }

    internal void UpgradeWeapon(UpgradeData upgradeData)
    {
        WeaponBase weaponToUpgrade = weapons.Find(wd => wd.weaponData == upgradeData.weaponData);
        weaponToUpgrade.Upgrade(upgradeData);

        int nextUpgradeIndex = (++upgradeData.weaponData.currentLevel) - 1;

        if (nextUpgradeIndex < 5)
        {
            level.AddUpgradesIntoTheListOfAvailableUpgrades(upgradeData.weaponData.upgrades[nextUpgradeIndex]);
        }

    }
}

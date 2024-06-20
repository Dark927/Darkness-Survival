using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    public float minDamage;
    public float maxDamage;
    public float timeToAttack;
    public int numberOfAttack;

    public WeaponStats(float minDamage, float maxDamage, float timeToAttack, int numberOfAttack)
    {
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.timeToAttack = timeToAttack;

        if (numberOfAttack != 0) this.numberOfAttack = numberOfAttack;
        else this.numberOfAttack = 1;
    }

    internal void Replace(WeaponStats weaponUpgradeStats)
    {
        minDamage = weaponUpgradeStats.minDamage;
        maxDamage = weaponUpgradeStats.maxDamage;

        if (weaponUpgradeStats.numberOfAttack != 0) numberOfAttack = weaponUpgradeStats.numberOfAttack;

        timeToAttack = weaponUpgradeStats.timeToAttack;
    }
}

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string Name;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;

    [HideInInspector]
    public int currentLevel = 0;

    public List<UpgradeData> upgrades;
}

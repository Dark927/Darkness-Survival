using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData weaponData;
    public WeaponStats weaponStats;

    float timer;

    public void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0f)
        {
            Attack();
            timer = weaponStats.timeToAttack;
        }
    }

    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;
        weaponStats = new WeaponStats(wd.stats.minDamage, wd.stats.maxDamage, wd.stats.timeToAttack, wd.stats.numberOfAttack);
    }

    public abstract void Attack();

    public virtual void PostDamage(float damage, Vector3 targetPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.Replace(upgradeData.weaponUpgradeStats);
    }
}

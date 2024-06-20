using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    // Values 

    public float speedPercent;
    public float maxHpPercent;
    public float firstHitDmgPercent;
    public float secondHitDmgPercent;
    public float armorPercent;
    public float collectionRadius;


    public PlayerStats
        (
        float speedPercent, 
        float maxHpPercent, 
        int firstHitDmgPercent, 
        float secondHitDmgPercent, 
        float armorPercent, 
        float collectionRadius
        )
    {
        this.speedPercent = speedPercent;
        this.maxHpPercent = maxHpPercent;
        this.firstHitDmgPercent = firstHitDmgPercent;
        this.secondHitDmgPercent = secondHitDmgPercent;
        this.armorPercent = armorPercent;
        this.collectionRadius = collectionRadius;
    }
}

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public string Name;

    [HideInInspector]
    public int currentLevel = 0;

    public List<UpgradeData> upgrades;
}

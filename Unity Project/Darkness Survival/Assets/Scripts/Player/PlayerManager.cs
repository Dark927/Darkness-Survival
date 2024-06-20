using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerMove playerMove;
    Character player;
    ControlCyberSword cyberSword;
    SoulMagnet soulMagnet;

    Level level;


    private void Awake()
    {
        level = GetComponent<Level>();
    }

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        player = GetComponent<Character>();
        cyberSword = GetComponentInChildren<ControlCyberSword>();
        soulMagnet = GetComponentInChildren<SoulMagnet>();
    }

    public void AddCharacterUpgrade(UpgradeData upgradeData)
    {
        upgradeData.playerData.currentLevel = 0;

        UpgradeCharacter(upgradeData);
    }

    public void UpgradeCharacter(UpgradeData upgradeData)
    {
        int nextUpgradeIndex = (++upgradeData.playerData.currentLevel) - 1;
       
        ////////////////////////////////////////////////////////
        // Check upgrades 
        ////////////////////////////////////////////////////////

        PlayerStats stats = upgradeData.playerUpgradeStats;

        // Speed Upgrade
        if (stats.speedPercent != 0)
        {
            playerMove.SPEED = (playerMove.SPEED / 100f) * stats.speedPercent;
        }

        // MaxHP Upgrade
        else if (stats.maxHpPercent != 0)
        {
            player.MAX_HP = (player.MAX_HP / 100f) * stats.maxHpPercent;
        }

        // First Hit Damage Upgrade
        else if (stats.firstHitDmgPercent != 0 && stats.secondHitDmgPercent != 0)
        {
            cyberSword.FIRST_HIT_DAMAGE = (cyberSword.FIRST_HIT_DAMAGE / 100f) * stats.firstHitDmgPercent;
            cyberSword.SECOND_HIT_DAMAGE = (cyberSword.SECOND_HIT_DAMAGE / 100f) * stats.secondHitDmgPercent;
        }       

        // Armor Upgrade

        else if(stats.armorPercent != 0)
        {
            player.ARMOR = (player.ARMOR / 100f) * stats.armorPercent;
        }

        // Soul Magnet Upgrade

        else if(stats.collectionRadius != 0)
        {
            soulMagnet.MAGNET_RADIUS = (soulMagnet.MAGNET_RADIUS / 100f) * (stats.collectionRadius * 100);

            // Check last lvl condition 

            if(nextUpgradeIndex >= 5)
            {
                level.isSoulMagnetFull = true;
            }
        }

        ////////////////////////////////////////////////////////
        
        // Next Upgrade

        if (nextUpgradeIndex < 5)
        {
            level.AddUpgradesIntoTheListOfAvailableUpgrades(upgradeData.playerData.upgrades[nextUpgradeIndex]);
        }
    }
}

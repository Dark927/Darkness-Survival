using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Attributes 

    int level = 1;
    int experience = 0;
    [SerializeField] ExperienceBar experienceBar;
    [SerializeField] UpgradePanelManager upgradePanel;

    List<UpgradeData> selectedUpgrades;

    [SerializeField] List<UpgradeData> startUpgrades;
    [SerializeField] List<UpgradeData> acquiredUpgrades;
    
    List<UpgradeData> upgrades;

    WeaponManager weaponManager;
    PlayerManager playerManager;


    public bool isSoulMagnetFull = false;

    // Methods 

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        playerManager = GetComponent<PlayerManager>();
    }


    private void Start()
    {
        upgrades = new List<UpgradeData>();
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        experienceBar.SetLevelText(level);
        AddUpgradesIntoTheListOfAvailableUpgrades(startUpgrades);
    }

    // Level Up variable

    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }

    // Experience 

    public void AddExperience(int amount)
    {
        if(isSoulMagnetFull)
        {
            amount = (amount / 100) * 105;
        }
        experience += amount;
        CheckLevelUp();
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }

    // CheckLevelUp

    public void CheckLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if (selectedUpgrades == null) { selectedUpgrades = new List<UpgradeData>(); }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(3));

        if(selectedUpgrades.Count > 0) upgradePanel.OpenPanel(selectedUpgrades);

        experience -= TO_LEVEL_UP;
        level += 1;
        experienceBar.SetLevelText(level);
    }

    //////////////
    // Upgrades //
    //////////////

    public void Upgrade(int selectedUpgradeID)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeID];

        if(acquiredUpgrades == null)
        {
            acquiredUpgrades = new List<UpgradeData>();
        }

        // Check upgrade type and apply

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                break;


            case UpgradeType.CharacterUpgrade:
                playerManager.UpgradeCharacter(upgradeData);
                break;


            case UpgradeType.WeaponUnlock:
                weaponManager.AddWeapon(upgradeData.weaponData);
                break;


            case UpgradeType.CharacterUpgradeUnlock:
                playerManager.AddCharacterUpgrade(upgradeData);
                break;

        }

        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> updradeList = new List<UpgradeData>();

        if(count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        while (updradeList.Count < count)
        {
            UpgradeData potentialUpgrade = upgrades[Random.Range(0, upgrades.Count)];

            if (!updradeList.Contains(potentialUpgrade))
            {
                updradeList.Add(potentialUpgrade);
            }
        }

        return updradeList;
    }

    public void AddUpgradesIntoTheListOfAvailableUpgrades(UpgradeData upgradeToAdd)
    {
        this.upgrades.Add(upgradeToAdd);
    }    
    
    public void AddUpgradesIntoTheListOfAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        this.upgrades.AddRange(upgradesToAdd);
    }
}

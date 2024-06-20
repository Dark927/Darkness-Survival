using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataContainer : ScriptableObject
{
    public int energyAcquired;

    public List<bool> stageCompletion;

    public void StageComplete(int i)
    {
        stageCompletion[i] = true;
    }

    // Save data 

    public void SaveEnergy()
    {
        PlayerPrefs.SetInt("EnergyAcquired", energyAcquired);
        PlayerPrefs.Save();
    }

    // Load data

    public void LoadEnergy()
    {
        energyAcquired = PlayerPrefs.GetInt("EnergyAcquired", 0);
    }
}

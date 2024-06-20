using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnergyPickUp : MonoBehaviour, IPickUpObject
{
    int count = 1;

    public void OnPickUp(Character character)
    {
        character.lightEnergy.Add(count);
    }
}

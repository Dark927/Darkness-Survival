using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] int amount;

    public void OnPickUp(Character character)
    {
        character.level.AddExperience(amount);
    }
}

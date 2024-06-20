using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] float healAmount;

    public void OnPickUp(Character character)
    {
        character.Heal(healAmount);
    }
}

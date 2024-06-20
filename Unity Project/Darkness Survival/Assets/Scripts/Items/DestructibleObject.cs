using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDamageable
{
    [SerializeField] float objectHp = 2f;

    public void TakeDamage(float damage)
    {
        objectHp -= damage;

        if(objectHp <= 0)
        {
            Destroy(gameObject);
            GetComponent<DropOnDestroy>().CheckDrop();
        }
    }
}

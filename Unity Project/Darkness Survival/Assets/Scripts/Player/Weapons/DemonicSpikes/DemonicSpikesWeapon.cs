using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicSpikesWeapon : WeaponBase
{
    PlayerMove playerMove;

    [SerializeField] GameObject spikePrefab;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    public override void Attack()
    {

        for (int i = 0; i < weaponStats.numberOfAttack; ++i)
        {
            GameObject thrownSpike = Instantiate(spikePrefab);

            float damage = weaponStats.maxDamage;

            thrownSpike.GetComponent<DemonicSpikeProjectile>().SetDamage(damage);

            thrownSpike.transform.position = new Vector3(
            transform.position.x,
            transform.position.y + 0.4f,
            transform.position.z);

            // For direct attacks 

            //thrownSpike.GetComponent<DemonicSpikeProjectile>().SetDirection(playerMove.lastHorizontalVector >= 0 ? 1 : -1, 0f);

            // For random direction 

            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            thrownSpike.GetComponent<DemonicSpikeProjectile>().SetDirection(randomX, randomY);
        }
    }
}

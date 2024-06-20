using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] Vector2 spawnArea;
    [SerializeField] Vector2 spawnRange;
    [SerializeField] float spawnTimer;
    GameObject player;
    //float timer;


    //// Boss
    //List<Monsters> bossMonstersList;
    //float totalBossHealth;
    //float currentBossHealth;

    //Slider bossHealthBar;

    private void Start()
    {
        //bossHealthBar = FindObjectOfType<BossHPBar>(true).GetComponent<Slider>();
        player = GameManager.instance.playerTransform.gameObject;
    }

    //private void Update()
    //{
    //timer -= Time.deltaTime;
    //if (timer < 0f)
    //{
    //    SpawnEnemy();
    //    timer = spawnTimer;
    //}

    //UpdateBossHealth();
    //}

    //private void UpdateBossHealth()
    //{
    //    if(bossMonstersList == null) { return; }
    //    if(bossMonstersList.Count == 0) { return; }

    //    currentBossHealth = 0f;

    //    for(int i = 0; i < bossMonstersList.Count; ++i)
    //    {
    //        currentBossHealth += bossMonstersList[i].HP;
    //    }
    //    bossHealthBar.value = currentBossHealth;
    //}

    public void SpawnEnemy(Monsters monstersToSpawn)
    {
        Vector3 position = UtilityTools.GenerateRandomPositionSquarePattern(spawnArea, spawnRange);

        position += player.transform.position;

        // Spawn Main Enemy object

        GameObject newEnemy = Instantiate(monstersToSpawn.gameObject, position, Quaternion.identity, gameObject.transform);

        // Check and set start side 

        if (player.transform.position.x - position.x < 0f)
        {
            newEnemy.transform.localScale = new Vector3(
                newEnemy.transform.localScale.x * -1,
                newEnemy.transform.localScale.y,
                newEnemy.transform.localScale.z
                );
        }

        newEnemy.GetComponent<Monsters>().SetTarget(player);

        //if (isBoss == true)
        //{
        //    SpawnBossEnemy(newEnemy.GetComponent<Monsters>());
        //}
    }
    
    /// <summary>
    /// Spawn enemy Boss
    /// </summary>
    /// <param name="bossToSpawn">Boss details container</param>
    public void SpawnBoss(GameObject bossToSpawn)
    {
        Vector3 position = UtilityTools.GenerateRandomPositionSquarePattern(spawnArea, spawnRange);

        position += player.transform.position;

        // Spawn Main Enemy object

        GameObject newBoss = Instantiate(bossToSpawn, position, Quaternion.identity, gameObject.transform);

        // Check and set start side 

        Transform bossTransform = newBoss.GetComponentInChildren<Monsters>().gameObject.transform;

        if (player.transform.position.x - bossTransform.position.x < 0f)
        {
            bossTransform.localScale = new Vector3(
                bossTransform.localScale.x * -1,
                bossTransform.localScale.y,
                bossTransform.localScale.z
                );
        }

        bossTransform.gameObject.GetComponent<Monsters>().SetTarget(player);
    }




    //private void SpawnBossEnemy(Monsters newBoss)
    //{
    //    if(bossMonstersList == null) { bossMonstersList = new List<Monsters>(); }
    //    bossMonstersList.Add(newBoss);

    //    totalBossHealth += newBoss.MAX_HP;
    //    bossHealthBar.maxValue = totalBossHealth;
    //    bossHealthBar.gameObject.SetActive(true);
    //}
}

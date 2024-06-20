using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    [SerializeField] EnemiesManager enemiesManager;

    StageTimer stageTimer;
    int eventIndexer;

    PlayerWinManager playerWin;

    private void Awake()
    {
        stageTimer = GetComponent<StageTimer>();
    }

    private void Start()
    {
        playerWin = FindObjectOfType<PlayerWinManager>();
    }

    private void Update()
    {
        if (eventIndexer >= stageData.stageEvents.Count) { return; }

        if (stageTimer.time > stageData.stageEvents[eventIndexer].time)
        {
            switch (stageData.stageEvents[eventIndexer].eventType)
            {
                case StageEventType.SpawnEnemy:
                    SpawnEnemy();
                    break;

                case StageEventType.SpawnObject:
                    SpawnObject();
                    break;

                case StageEventType.WinStage:
                    WinStage();
                    break;

                case StageEventType.SpawnEnemyBoss:
                    SpawnEnemyBoss();
                    break;
            }

            Debug.Log(stageData.stageEvents[eventIndexer].message);
            eventIndexer += 1;
        }
    }

    private void SpawnEnemyBoss()
    {
        for (int i = 0; i < stageData.stageEvents[eventIndexer].count; ++i)
        {
            enemiesManager.SpawnBoss(stageData.stageEvents[eventIndexer].bossToSpawn);
        }
    }

    private void WinStage()
    {
        playerWin.Win();
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < stageData.stageEvents[eventIndexer].count; ++i)
        {
            enemiesManager.SpawnEnemy(stageData.stageEvents[eventIndexer].monstersToSpawn);
        }
    }

    private void SpawnObject()
    {
        for (int i = 0; i < stageData.stageEvents[eventIndexer].count; ++i)
        {
            Vector3 positionToSpawn = GameManager.instance.playerTransform.position;
            positionToSpawn += UtilityTools.GenerateRandomPositionSquarePattern(
                new Vector2(5f, 5f),
                new Vector2(0f, 0f));

            SpawnManager.instance.SpawnObject(
                positionToSpawn,
                stageData.stageEvents[eventIndexer].objectToSpawn
                );
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;   
    }

    public void SpawnObject(Vector3 worldPosition, GameObject toSpawn)
    {
        Vector3 spawnPosition = new Vector3(worldPosition.x, worldPosition.y - 0.05f, worldPosition.z);
        Instantiate(toSpawn, spawnPosition, Quaternion.identity, transform);
    }
}

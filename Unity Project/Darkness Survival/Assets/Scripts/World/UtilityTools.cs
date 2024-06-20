using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityTools : MonoBehaviour
{
    /// <summary>
    /// Use this function to generate a random position within a given range
    /// </summary>
    /// <param name="spawnArea">Distance from which to create a position</param>
    /// <param name="spawnRange">Random spread from spawnArea</param>
    /// <returns>Returns random position in your Square</returns>
    public static Vector3 GenerateRandomPositionSquarePattern(Vector2 spawnArea, Vector2 spawnRange)
    {
        Vector3 position = new Vector3();

        float axisSide = Random.value > 0.5f ? -1f : 1f;

        if (Random.value > 0.5f)
        {
            position.x = Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * axisSide + Random.Range(0, spawnRange.y);
        }
        else
        {
            position.x = spawnArea.x * axisSide + Random.Range(0, spawnRange.x);
            position.y = Random.Range(-spawnArea.y, spawnArea.y);
        }

        position.z = 0;

        return position;
    }
}

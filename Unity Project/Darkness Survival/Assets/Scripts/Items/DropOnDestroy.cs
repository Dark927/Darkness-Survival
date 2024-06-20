using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] List<GameObject> dropItemPrefab;
    [SerializeField] [Range(0f, 1f)] float chance = 1f;

    // Flag to check if the application is quitting.
    private static bool isQuitting = false;

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public void CheckDrop()
    {
        // Check if the game is quitting or the object is being destroyed due to scene unloading.
        if (isQuitting || !gameObject.scene.isLoaded)
        {
            return;
        }

        if(dropItemPrefab.Count <= 0)
        {
            Debug.LogWarning("List of drop items is empty!");
            return;
        }

        if (dropItemPrefab != null && Random.value < chance)
        {
            GameObject toDrop = dropItemPrefab[Random.Range(0, dropItemPrefab.Count)];

            if(toDrop == null)
            {
                Debug.LogWarning("DropOnDestroy, reference to dropped item is null! Check the prefab of the object which drops items on destroy!");
                return;
            }

            SpawnManager.instance.SpawnObject(transform.position, toDrop);
        }
    }
}

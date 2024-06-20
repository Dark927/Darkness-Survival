using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    Transform playerTransform;
    Vector2Int currentPlayerTilePosition = new Vector2Int(0,0);
    [SerializeField] Vector2Int playerTilePosition;

    Vector2Int onTileGridPosition;
    [SerializeField] float blockSize = 36;
    GameObject[,] blockTiles;

    [SerializeField] int blockTileHorizontalCount;
    [SerializeField] int blockTileVerticalCount;

    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    private void Awake()
    {
        blockTiles = new GameObject[blockTileHorizontalCount, blockTileVerticalCount];
        currentPlayerTilePosition = playerTilePosition;
    }

    private void Start()
    {
        playerTransform = GameManager.instance.playerTransform;
        UpdateTilesOnScreen();
    }

    private void Update()
    {
        playerTilePosition.x = (int)(playerTransform.position.x / blockSize);
        playerTilePosition.y = (int)(playerTransform.position.y / blockSize);

        playerTilePosition.x -= playerTransform.position.x < 0 ? 1 : 0;
        playerTilePosition.y -= playerTransform.position.y < 0 ? 1 : 0;

        if(currentPlayerTilePosition != playerTilePosition)
        {
            currentPlayerTilePosition = playerTilePosition;

            onTileGridPosition.x = calculatePositionOnAxis(onTileGridPosition.x, true);
            onTileGridPosition.y = calculatePositionOnAxis(onTileGridPosition.y, false);
            UpdateTilesOnScreen();
        }
    }

    private void UpdateTilesOnScreen()
    {
        for(int pov_x = -(fieldOfVisionWidth/2); pov_x <= fieldOfVisionWidth/2; pov_x++)
        {
            for(int pov_y = -(fieldOfVisionHeight/2); pov_y <= fieldOfVisionHeight/2; pov_y++)
            {
                int tileToUpdate_x = calculatePositionOnAxis(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = calculatePositionOnAxis(playerTilePosition.y + pov_y, false);

                GameObject tile = blockTiles[tileToUpdate_x, tileToUpdate_y];
                Vector3 newPosition = calculateTilePosition(
                    playerTilePosition.x + pov_x,
                    playerTilePosition.y + pov_y
                    );

                if (newPosition != tile.transform.position)
                {
                    tile.transform.position = newPosition;
                    blockTiles[tileToUpdate_x, tileToUpdate_y].GetComponent<BlockTile>().Spawn();
                }

            }
        }
    }

    private Vector3 calculateTilePosition(int x, int y)
    {
        return new Vector3(x * blockSize, y * blockSize, 0f);
    }

    private int calculatePositionOnAxis(float currentValue, bool horizontal)
    {
        if(horizontal)
        {
            if(currentValue >= 0)
            {
                currentValue = currentValue % blockTileHorizontalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = blockTileHorizontalCount - 1 
                    + currentValue % blockTileHorizontalCount;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % blockTileVerticalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = blockTileVerticalCount - 1
                    + currentValue % blockTileVerticalCount;
            }
        }

        return (int)currentValue;
    }

    public void Add(GameObject tileGameObject, Vector2Int tilePosition)
    {
        blockTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }
}

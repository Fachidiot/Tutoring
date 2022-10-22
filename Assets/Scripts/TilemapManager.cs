using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapManager : MonoBehaviour
{
    public Transform player;    // 플레이어의 위치값
    public GameObject[] tilemapObjects; // 타일 맵들
    public int tileHorizontalCount; // 타일 수평 갯수
    public int tileVerticalCount;   // 타일 수직 갯수
    public float tileSize;      // 타일 크기

    GameObject[,] tilemaps;  // 타일 배열
    [SerializeField] Vector2Int currentTilePosition = new Vector2Int(0, 0);  // 현재 타일의 위치
    [SerializeField] Vector2Int playerTilePosition;          // 플레이어가 있는 타일
    
    void Awake()
    {
        tilemaps = new GameObject[tileHorizontalCount, tileVerticalCount];
        int count = 0;
        for (int i = 0; i < tilemaps.GetLength(0); i++)
        {
            for (int j = 0; j < tilemaps.GetLength(1); j++)
            {
                tilemaps[j, i] = tilemapObjects[count++];
                // Debug.Log(tilemaps[i,j]);
            }
        }
    }

    void Update()
    {
        playerTilePosition.x = (int)(player.position.x / tileSize);
        playerTilePosition.y = (int)(player.position.y / tileSize);

        playerTilePosition.x -= player.position.x < 0 ? 1 : 0;
        playerTilePosition.y -= player.position.y < 0 ? 1 : 0;

        if (currentTilePosition != playerTilePosition) {
            currentTilePosition = playerTilePosition;
            
            UpdateTilesOnScreen();
        }
    }

    void UpdateTilesOnScreen() {
        for (int pov_x = -(tileHorizontalCount / 2); pov_x <= tileHorizontalCount / 2; pov_x++) {
            for (int pov_y = -(tileVerticalCount / 2); pov_y <= tileVerticalCount / 2; pov_y++) {
                int tileToUpdate_x = CalculatePositionOnAxisWithWrap(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxisWithWrap(playerTilePosition.y + pov_y, false);

                GameObject tile = tilemaps[tileToUpdate_x, tileToUpdate_y];
                tile.transform.position = CalculateTilePosition(playerTilePosition.x + pov_x, playerTilePosition.y + pov_y);
            }
        }
    }

    Vector3 CalculateTilePosition(int x, int y) {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    int CalculatePositionOnAxisWithWrap(float currentValue, bool horizontal) {
        if (horizontal) {
            if (currentValue >= 0) {
                currentValue = currentValue % tileHorizontalCount;
            }
            else {
                currentValue += 1;
                currentValue = tileHorizontalCount - 1 + currentValue % tileHorizontalCount;
            }
        } else {
            if (currentValue >= 0) {
                currentValue = currentValue % tileVerticalCount;
            }
            else {
                currentValue += 1;
                currentValue = tileVerticalCount - 1 + currentValue % tileVerticalCount;
            }
        }

        return (int)currentValue;
    }
}

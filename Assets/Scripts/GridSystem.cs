using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    int width;
    int height;
    float cellSize;
    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.up * 0.2f, Color.white, 100f);
            }
        }
    }
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z)*cellSize;
    }
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        // Debug.Log("FloorToInt x="+Mathf.FloorToInt(worldPosition.x / cellSize)+" z="+Mathf.FloorToInt(worldPosition.z / cellSize));
        // Debug.Log("RoundToInt x="+Mathf.RoundToInt(worldPosition.x / cellSize)+" z="+Mathf.RoundToInt(worldPosition.z / cellSize));
        return new GridPosition(Mathf.FloorToInt(worldPosition.x / cellSize), Mathf.FloorToInt(worldPosition.z / cellSize));
    }
}

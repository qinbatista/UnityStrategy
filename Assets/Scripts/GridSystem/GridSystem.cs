using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridSystem
{
    int width;
    int height;
    float cellSize;
    GridObject[,] gridObjArray;

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridObjArray = new GridObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.up * 0.2f, Color.white, 100f);
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjArray[x, z] = new GridObject(this, gridPosition);
            }
        }
    }
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        // Debug.Log("FloorToInt x="+Mathf.FloorToInt(worldPosition.x / cellSize)+" z="+Mathf.FloorToInt(worldPosition.z / cellSize));
        // Debug.Log("RoundToInt x="+Mathf.RoundToInt(worldPosition.x / cellSize)+" z="+Mathf.RoundToInt(worldPosition.z / cellSize));
        return new GridPosition(Mathf.FloorToInt(worldPosition.x / cellSize), Mathf.FloorToInt(worldPosition.z / cellSize));
    }
    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(x, z), Quaternion.identity);
                GridDebugObj gridDebugObj =  debugTransform.GetComponent<GridDebugObj>();
                gridDebugObj.SetGridObject(GetGridObject(new GridPosition(x, z)));
                gridDebugObj.SetText("(" + x + "," + z + ")");
            }
        }
    }
    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjArray[gridPosition.x, gridPosition.z];
    }
}

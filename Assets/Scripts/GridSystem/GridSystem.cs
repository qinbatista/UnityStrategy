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
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjArray[x, z] = new GridObject(this, gridPosition);
                Debug.DrawLine(GetWorldPosition(gridPosition), GetWorldPosition(gridPosition) + Vector3.up * 0.2f, Color.white, 100f);
            }
        }
    }
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        // Debug.Log("FloorToInt x="+Mathf.FloorToInt(worldPosition.x / cellSize)+" z="+Mathf.FloorToInt(worldPosition.z / cellSize));
        // Debug.Log("RoundToInt x="+Mathf.RoundToInt(worldPosition.x / cellSize)+" z="+Mathf.RoundToInt(worldPosition.z / cellSize));
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / cellSize), Mathf.RoundToInt(worldPosition.z / cellSize));
    }
    public void CreateDebugObjects(Transform PositionTextPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Transform positionTextTransform = GameObject.Instantiate(PositionTextPrefab, GetWorldPosition(new GridPosition(x,z)), Quaternion.identity);
                GridPositionText gridDebugObj =  positionTextTransform.GetComponent<GridPositionText>();
                gridDebugObj.SetGridObject(GetGridObject(new GridPosition(x, z)));
                gridDebugObj.SetText("(" + x + "," + z + ")");
                gridObjArray[x, z].SetGridPositionText(gridDebugObj);
            }
        }
    }
    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjArray[gridPosition.x, gridPosition.z];
    }
    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < width && gridPosition.z >= 0 && gridPosition.z < height;
    }
    public void SetPositionText(GridPosition gridPosition)
    {
        gridObjArray[gridPosition.x, gridPosition.z].SetText();
    }
}

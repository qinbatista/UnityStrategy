using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
public class GridSystem<TGridObject>
{
    int width;
    int height;
    float cellSize;
    TGridObject[,] gridObjArray;
    public GridSystem(int width, int height, float cellSize, Func<GridSystem<TGridObject>, GridPosition, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridObjArray = new TGridObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjArray[x, z] = createGridObject(this, gridPosition);
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
                GridPosition gridPosition = new GridPosition(x,z);
                Transform debugTransform = GameObject.Instantiate(PositionTextPrefab, GetWorldPosition(new GridPosition(x,z)), Quaternion.identity);
                GridPositionText gridDebugObj =  debugTransform.GetComponent<GridPositionText>();
                gridDebugObj.SetGridObject(GetGridObject(gridPosition));


                // Transform positionTextTransform = GameObject.Instantiate(PositionTextPrefab, GetWorldPosition(new GridPosition(x,z)), Quaternion.identity);

                // gridDebugObj.SetGridObject(GetGridObject(new GridPosition(x, z)) as GridObject);
                // gridDebugObj.SetText("(" + x + "," + z + ")");
                // (gridObjArray[x, z] as GridObject).SetGridPositionText(gridDebugObj);
            }
        }
    }
    public TGridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjArray[gridPosition.x, gridPosition.z];
    }
    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < width && gridPosition.z >= 0 && gridPosition.z < height;
    }
    public void SetPositionText(GridPosition gridPosition)
    {
        (gridObjArray[gridPosition.x, gridPosition.z] as GridObject).SetText();
    }
    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
}

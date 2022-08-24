using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GridVisual : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform gridSystemVisualSinglePrefab;
    GridVisualSingle[,] GridVisualSingleArray;
    public static GridVisual Instance { get; private set; }
    [Serializable]
    public struct GridVisualTypeMaterial
    {
        public GridVisualType gridVisualType;
        public Material material;
    }
    public enum GridVisualType
    {
        White,
        Blue,
        Red,
        Yellow,
        RedSoft,
    }
    [SerializeField] List<GridVisualTypeMaterial> gridVisualTypeMaterials;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        GridVisualSingleArray = new GridVisualSingle[GridManager.Instance.GetWidth(), GridManager.Instance.GetHeight()];

        for (int x = 0; x < GridManager.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < GridManager.Instance.GetHeight(); z++)
            {
                Transform gridSystemVisualSingle = Instantiate(gridSystemVisualSinglePrefab, GridManager.Instance.GetWorldPosition(new GridPosition(x, z)), Quaternion.identity, transform);
                GridVisualSingleArray[x, z] = gridSystemVisualSingle.GetComponent<GridVisualSingle>();
            }
        }
        UnitActionSystem.Instance.OnSelectActionEvent += UnitActionSystem_OnSelectActionEvent;
        GridManager.Instance.OnAnyUnitMovedGridPosition += GridManager_OnAnyUnitMovedGridPosition;
        UpdateGridVisual();
    }
    public void HideAllGridPosition()
    {
        for (int x = 0; x < GridManager.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < GridManager.Instance.GetHeight(); z++)
            {
                GridVisualSingleArray[x, z].Hide();
            }
        }
    }
    public void ShowAllGridPosition(List<GridPosition> gridPositionList, GridVisualType gridVisualType)
    {
        Debug.Log("a:"+GridVisualSingleArray.Length.ToString());
        foreach (GridPosition gridPosition in gridPositionList)
        {
            GridVisualSingleArray[gridPosition.x, gridPosition.z].Show(GetGridVisualTypeMaterial(gridVisualType));
        }
    }
    void ShowPositionRange(GridPosition gridPosition, int range, GridVisualType gridVisualType)
    {
        List<GridPosition> gridPositionList = new List<GridPosition>();
        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition testGridPosition = gridPosition + new GridPosition(x, z);
                if (!GridManager.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > range) continue;
                gridPositionList.Add(testGridPosition);
            }
        }
        Debug.Log(gridPositionList);
        Debug.Log(gridVisualType);
        ShowAllGridPosition(gridPositionList,gridVisualType);
    }

    // void Update()
    // {
    //     UpdateGridVisual();
    // }
    void UpdateGridVisual()
    {
        HideAllGridPosition();
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
        GridVisualType gridVisualType;
        switch (selectedAction)
        {

            case MoveAction moveAction:
                gridVisualType = GridVisualType.White;
                break;
            case SpinAction spinAction:
                gridVisualType = GridVisualType.Blue;
                break;
            case ShootAction shootAction:
                gridVisualType = GridVisualType.Red;
                ShowPositionRange(selectedUnit.GetGridPosition(),shootAction.GetMaxShootDistance(), GridVisualType.RedSoft);
                break;
            default:
                gridVisualType = GridVisualType.White;
                break;
        }
        ShowAllGridPosition(selectedAction.GetValidActionGridPositionList(), gridVisualType);
    }
    void UnitActionSystem_OnSelectActionEvent(Unit unit)
    {
        UpdateGridVisual();
    }
    void GridManager_OnAnyUnitMovedGridPosition()
    {
        UpdateGridVisual();
    }
    Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
    {
        foreach (var item in gridVisualTypeMaterials)
        {
            if (item.gridVisualType == gridVisualType)
            {
                return item.material;
            }
        }
        Debug.LogError("could not find gridVisualType=" + gridVisualType);
        return null;
    }
}

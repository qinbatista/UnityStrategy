using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    [SerializeField] Vector2 gridSize;
    [SerializeField][Range(1, 10)] int cellSize;
    [SerializeField] Transform gridPrefab;
    GridSystem gridSystem;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        gridSystem = new GridSystem((int)gridSize.x, (int)gridSize.y, (float)cellSize);
        gridSystem.CreateDebugObjects(gridPrefab);
    }
    void Start()
    {
        // Debug.Log(new GridPosition(5, 5));
    }

    void Update()
    {
        // Debug.Log(gridSystem.GetGridPosition(MouseController.Instance.GetWorldPosition()));
    }
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        gridSystem.GetGridObject(gridPosition).AddUnit(unit);
    }
    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition) => gridSystem.GetGridObject(gridPosition).GetUnitList();
    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit) => gridSystem.GetGridObject(gridPosition).RemoveUnit(unit);
    public GridPosition GetGridPosition(Vector3 wordPosition) => gridSystem.GetGridPosition(wordPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);
    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);
    public void SetGridText(GridPosition gridPosition) => gridSystem.SetPositionText(gridPosition);
    public void UnitMoveGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
        SetGridText(fromGridPosition);
        SetGridText(toGridPosition);
    }
    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition) => gridSystem.GetGridObject(gridPosition).HasAnyUnit();
    public int GetWidth() => gridSystem.GetWidth();
    public int GetHeight() => gridSystem.GetHeight();
}

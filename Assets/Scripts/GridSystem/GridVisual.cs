using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisual : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform gridSystemVisualSinglePrefab;
    GridVisualSingle[,] GridVisualSingleArray;
    public static GridVisual Instance { get; private set; }
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
    public void ShowAllGridPosition(List<GridPosition> gridPositionList)
    {
        foreach (var item in gridPositionList)
        {
            GridVisualSingleArray[item.x, item.z].Show();
        }
    }

    void Update()
    {
        UpdateGridVisual();
    }
    void UpdateGridVisual()
    {
        HideAllGridPosition();
        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
        ShowAllGridPosition(selectedAction.GetValidActionGridPositionList());
    }
}

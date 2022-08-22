using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    [SerializeField] Unit selectUnit;
    [SerializeField] LayerMask unitLayerMask;
    public event Action<Unit> OnSelectUnitEvent;
    void Awake()
    {
        if(Instance==null)Instance = this;
        else Destroy(gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) || Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnit()) return;
            GridPosition mouseGridPosition = GridManager.Instance.GetGridPosition(MouseController.Instance.GetWorldPosition());
            // Debug.Log("Move:"+mouseGridPosition);
            if(selectUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                selectUnit.GetMoveAction().SetTarget(mouseGridPosition);
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            selectUnit.GetSpinAction().Spin();
        }
    }
    bool TryHandleUnit()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.MaxValue, unitLayerMask))
        {
            if (hit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }
    void SetSelectedUnit(Unit unit)
    {
        selectUnit = unit;
        OnSelectUnitEvent?.Invoke(unit);
    }
    public Unit GetSelectedUnit() => selectUnit;
}

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
    bool isBusy;
    void Awake()
    {
        if(Instance==null)Instance = this;
        else Destroy(gameObject);
    }
    void Update()
    {
        if(isBusy) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnit()) return;
            GridPosition mouseGridPosition = GridManager.Instance.GetGridPosition(MouseController.Instance.GetWorldPosition());
            // Debug.Log("Move:"+mouseGridPosition);
            if(selectUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                selectUnit.GetMoveAction().SetTarget(mouseGridPosition,ClearBusy);
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            SetBusy();
            selectUnit.GetSpinAction().Spin(ClearBusy);
        }
    }
    private void SetBusy()
    {
        isBusy = true;
    }
    private void ClearBusy()
    {
        isBusy = false;
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

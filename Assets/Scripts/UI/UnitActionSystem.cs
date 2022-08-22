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
    BaseAction selectedAction;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        SetSelectedUnit(selectUnit);
    }
    void Update()
    {
        if (isBusy) return;
        if (TryHandleUnit()) return;
        HandleSelectedAction();
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
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.MaxValue, unitLayerMask))
            {
                if (hit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }
        return false;
    }
    void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = GridManager.Instance.GetGridPosition(MouseController.Instance.GetWorldPosition());
            switch (selectedAction)
            {
                case MoveAction moveAction:
                    if (moveAction.IsValidActionGridPosition(mouseGridPosition))
                    {
                        SetBusy();
                        moveAction.SetTarget(mouseGridPosition, ClearBusy);
                    }
                    break;
                case SpinAction spinAction:
                    SetBusy();
                    spinAction.Spin(ClearBusy);
                    break;
            }
        }
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
    }
    void SetSelectedUnit(Unit unit)
    {
        selectUnit = unit;
        SetSelectedAction(unit.GetMoveAction());
        OnSelectUnitEvent?.Invoke(unit);
    }
    public Unit GetSelectedUnit() => selectUnit;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    [SerializeField] Unit selectUnit;
    [SerializeField] LayerMask unitLayerMask;
    public event Action<Unit> OnSelectUnitEvent;
    public event Action<Unit> OnSelectActionEvent;
    public event Action<bool> OnBusyChangeEvent;
    public event Action OnActionStartEvent;
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
        if(EventSystem.current.IsPointerOverGameObject()) return;
        if(!TurnSystem.Instance.IsPlayerTurn()) return;
        if (TryHandleUnit()) return;
        HandleSelectedAction();
    }
    private void SetBusy()
    {
        isBusy = true;
        OnBusyChangeEvent?.Invoke(isBusy);
    }
    private void ClearBusy()
    {
        isBusy = false;
        OnBusyChangeEvent?.Invoke(isBusy);
    }
    bool TryHandleUnit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.MaxValue, unitLayerMask))
            {
                if (hit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    // Debug.Log("selectUnit.IsEnemy()=" +unit.IsEnemy()+" hit="+unit.transform.name+" selectUnit="+unit.isEnemy);
                    if(unit == selectUnit)
                    {
                        return false;
                    }
                    if(unit.IsEnemy())
                    {
                        return false;
                    }
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
            if(!selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                // Debug.Log("CanSpeedActionPoint return");
                return;
            }
            if(!selectUnit.TrySpeedActionPoints(selectedAction))
            {
                // Debug.Log("TrySpeedActionPoints return");
                return;
            }
            SetBusy();
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            OnActionStartEvent?.Invoke();
        }
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
        OnSelectActionEvent?.Invoke(selectUnit);
    }
    void SetSelectedUnit(Unit unit)
    {
        selectUnit = unit;
        SetSelectedAction(unit.GetMoveAction());
        OnSelectUnitEvent?.Invoke(unit);
    }
    public Unit GetSelectedUnit() => selectUnit;
    public BaseAction GetSelectedAction() => selectedAction;

}

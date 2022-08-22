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
                    if(unit == selectUnit)
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
            // Debug.Log("aaa mouseGridPosition="+mouseGridPosition);
            if(selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            }
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

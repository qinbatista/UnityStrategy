using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UnityActionSystemUI : MonoBehaviour
{
    [SerializeField] Transform actionButtonPrefab;
    [SerializeField] Transform actionButtonContainerTransform;
    List<ActionButtonUI> actionButtonUIList;
    [SerializeField] TextMeshProUGUI ActionPoint;
    void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }
    void Start()
    {
        UnitActionSystem.Instance.OnSelectUnitEvent += UnitActionSystem_OnSelectUnitEvent;
        UnitActionSystem.Instance.OnSelectActionEvent += UnitActionSystem_OnSelectActionEvent;
        UnitActionSystem.Instance.OnActionStartEvent += UnitActionSystem_OnActionStartEvent;
        TurnSystem.Instance.onTurnChanged += TurnSystem_OnTurnChanged;
        Unit.OnAnyPointsChanged += Unit_OnAnyPointsChanged;
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoint();
    }
    void CreateUnitActionButtons()
    {
        foreach (var item in actionButtonContainerTransform)
        {
            Destroy((item as Transform).gameObject);
        }
        actionButtonUIList.Clear();
        Unit selectUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction item in selectUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(item);
            actionButtonUIList.Add(actionButtonUI);
        }
    }
    void UnitActionSystem_OnSelectUnitEvent(Unit unit)
    {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoint();
    }

    void UnitActionSystem_OnSelectActionEvent(Unit unit)
    {
        UpdateSelectedVisual();
    }
    void UnitActionSystem_OnActionStartEvent()
    {
        UpdateActionPoint();
    }
    void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI item in actionButtonUIList)
        {
            item.UpdateSelectedVisual();
        }
    }
    void UpdateActionPoint()
    {
        ActionPoint.text = "Action Points:" + UnitActionSystem.Instance.GetSelectedUnit().GetActionPoint();
    }
    void TurnSystem_OnTurnChanged()
    {
        UpdateActionPoint();
    }
    private void Unit_OnAnyPointsChanged()
    {
        UpdateActionPoint();
    }
}

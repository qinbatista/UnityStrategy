using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnityActionSystemUI : MonoBehaviour
{
    [SerializeField] Transform actionButtonPrefab;
    [SerializeField] Transform actionButtonContainerTransform;
    List<ActionButtonUI> actionButtonUIList;
    void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }
    void Start()
    {
        UnitActionSystem.Instance.OnSelectUnitEvent += UnitActionSystem_OnSelectUnitEvent;
        UnitActionSystem.Instance.OnSelectActionEvent += UnitActionSystem_OnSelectActionEvent;
        CreateUnitActionButtons();
        UpdateSelectedVisual();
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
    }

    void UnitActionSystem_OnSelectActionEvent(Unit unit)
    {
        UpdateSelectedVisual();
    }
    void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI item in actionButtonUIList)
        {
            item.UpdateSelectedVisual();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnityActionSystemUI : MonoBehaviour
{
    [SerializeField] Transform actionButtonPrefab;
    [SerializeField] Transform actionButtonContainerTransform;
    void Start()
    {
        CreateUnitActionButtons();
        UnitActionSystem.Instance.OnSelectUnitEvent+=UnitActionSystem_OnSelectUnitEvent;
    }
    void CreateUnitActionButtons()
    {
        foreach (var item in actionButtonContainerTransform)
        {
            Destroy((item as Transform).gameObject);
        }
        Unit selectUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction item in selectUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab,actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(item);
        }
    }
    void UnitActionSystem_OnSelectUnitEvent(Unit unit)
    {
        CreateUnitActionButtons();
    }
}

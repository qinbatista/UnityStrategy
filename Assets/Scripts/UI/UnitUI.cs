using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class UnitUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI actionPointsText;
    [SerializeField] Unit unit;
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] Image healthBarImage;
    void Start()
    {
        Unit.OnAnyPointsChanged += Unit_OnAnyPointsChanged;
        healthSystem.Damaged += healthSystem_Damaged;
        UpdateActionPointsText();
        UpdateHealthBar();
    }
    void UpdateActionPointsText()
    {
        actionPointsText.text = unit.GetActionPoint().ToString();
    }
    void Unit_OnAnyPointsChanged()
    {
        UpdateActionPointsText();
    }
    void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }
    void healthSystem_Damaged()
    {
        UpdateHealthBar();
    }

}

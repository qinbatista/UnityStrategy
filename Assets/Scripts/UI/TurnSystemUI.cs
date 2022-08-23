using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turnText;
    void Start()
    {
        TurnSystem.Instance.TurnChanged += TurnSystem_OnTurnChanged;
        UpdateTurnText();
    }
    public void TurnSystem_OnTurnChanged()
    {
        UpdateTurnText();
    }
    public void UpdateTurnText()
    {
        turnText.text = "TURN:" + TurnSystem.Instance.GetTurnNumber().ToString();
    }
}

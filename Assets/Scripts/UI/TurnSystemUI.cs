using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] GameObject enemyTurnVisualGameObject;
    [SerializeField] GameObject turnButton;
    void Start()
    {
        TurnSystem.Instance.onTurnChanged += TurnSystem_OnTurnChanged;
        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateTurnButtonVisual();
    }
    public void TurnSystem_OnTurnChanged()
    {
        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateTurnButtonVisual();
    }
    public void UpdateTurnText()
    {
        turnText.text = "TURN:" + TurnSystem.Instance.GetTurnNumber().ToString();
    }
    void UpdateEnemyTurnVisual()
    {
        enemyTurnVisualGameObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }
    void UpdateTurnButtonVisual()
    {
        turnButton.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }
}

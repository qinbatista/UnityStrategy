using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    float timer;
    void Start()
    {
        TurnSystem.Instance.onTurnChanged += TurnSystem_OnTurnChanged;
    }
    void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }
        timer -= Time.deltaTime;
        if(timer<=0f)
        {
            TurnSystem.Instance.NextTurn();
        }
    }
    void TurnSystem_OnTurnChanged()
    {
        timer = 2f;
    }
}

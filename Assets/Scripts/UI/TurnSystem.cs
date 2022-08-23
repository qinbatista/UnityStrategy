using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    int turnNumber = 1;
    public static TurnSystem Instance { get; private set; }
    public event Action TurnChanged;
    bool isPlayerTurn = true;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void NextTurn()
    {
        turnNumber++;
        isPlayerTurn = !isPlayerTurn;
        TurnChanged?.Invoke();
    }
    public int GetTurnNumber()
    {
        return turnNumber;
    }
    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}

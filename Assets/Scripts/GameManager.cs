using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Unit unit;
    void Start()
    {
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            unit.GetMoveAction().GetValidActionGridPositionList();
        }
    }
}

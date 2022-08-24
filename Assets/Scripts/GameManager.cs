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
            GridVisual.Instance.HideAllGridPosition();
            // GridVisual.Instance.ShowAllGridPosition(unit.GetMoveAction().GetValidActionGridPositionList());
        }
    }
}

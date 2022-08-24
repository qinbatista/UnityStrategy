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
        List<int> aList = new List<int>();
        aList.Add(1);
        aList.Add(2);
        aList.Add(3);
        if(Input.GetKeyDown(KeyCode.G))
        {
            aList.Sort((int a, int b)=>b-a);
            foreach (int item in aList)
            {
                Debug.Log(item);
            }
            GridVisual.Instance.HideAllGridPosition();
            // GridVisual.Instance.ShowAllGridPosition(unit.GetMoveAction().GetValidActionGridPositionList());
        }
    }
}

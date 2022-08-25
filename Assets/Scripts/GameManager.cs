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

        if (Input.GetKeyDown(KeyCode.G))
        {
            MyMethodClass<BaseAction> myClassInt = new MyMethodClass<BaseAction>(null);
            MyNormalClass<string> myClassString = new MyNormalClass<string>("string");
            // GridPosition mouseGridPosition = GridManager.Instance.GetGridPosition(MouseController.Instance.GetWorldPosition());
            // GridPosition startGridPosition = new GridPosition(0, 0);
            // List<GridPosition> gridPositionsList = PathFinding.Instance.FindPath(startGridPosition, mouseGridPosition);
            // for (int i = 0; i < gridPositionsList.Count - 1; i++)
            // {
            //     Debug.DrawLine(GridManager.Instance.GetWorldPosition(gridPositionsList[i]),
            //     GridManager.Instance.GetWorldPosition(gridPositionsList[i + 1]), Color.white, 10f);
            // }

            GridPosition mouseGridPosition = GridManager.Instance.GetGridPosition(MouseController.Instance.GetWorldPosition());
            GridPosition startGridPosition = new GridPosition(0, 0);

            List<GridPosition> gridPositionList = PathFinding.Instance.FindPath(startGridPosition, mouseGridPosition);

            for (int i = 0; i < gridPositionList.Count - 1; i++)
            {
                Debug.DrawLine(
                    GridManager.Instance.GetWorldPosition(gridPositionList[i]),
                    GridManager.Instance.GetWorldPosition(gridPositionList[i + 1]),
                    Color.white,
                    10f
                );
            }

        }
    }
    class MyNormalClass<T>
    {
        T i;
        public MyNormalClass(T i)
        {
            this.i = i;
            Debug.Log(i);
        }
    }
    class MyMethodClass<T> where T : BaseAction
    {
        T i;
        public MyMethodClass(T i)
        {
            this.i = i;
            Debug.Log(i);
        }
    }
}

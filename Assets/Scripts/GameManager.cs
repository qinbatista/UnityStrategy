using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector2 GridSize;
    [SerializeField][Range(1, 10)] int CellSize;
    GridSystem gridSystem;
    void Start()
    {
        gridSystem = new GridSystem((int)GridSize.x, (int)GridSize.y, (float)CellSize);
        Debug.Log(new GridPosition(5,5));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(MouseController.Instance.GetWorldPosition()));
    }
}

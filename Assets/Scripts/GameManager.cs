using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector2 gridSize;
    [SerializeField][Range(1, 10)] int cellSize;
    [SerializeField] Transform gridPrefab;
    GridSystem gridSystem;
    void Start()
    {
        gridSystem = new GridSystem((int)gridSize.x, (int)gridSize.y, (float)cellSize);
        gridSystem.CreateDebugObjects(gridPrefab);
        Debug.Log(new GridPosition(5,5));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(MouseController.Instance.GetWorldPosition()));
    }
}

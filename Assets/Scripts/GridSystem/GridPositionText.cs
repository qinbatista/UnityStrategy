using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridPositionText : MonoBehaviour
{
    GridObject gridObject;
    Unit unit;
    [SerializeField] TextMeshPro text;
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
    public void SetText(string text)
    {
        this.text.text = gridObject.ToString();
    }
}

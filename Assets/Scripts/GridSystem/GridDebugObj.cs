using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridDebugObj : MonoBehaviour
{
    GridObject gridObject;
    [SerializeField] TextMeshPro text;
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
    public void SetText(string text)
    {
        this.text.text = text;
    }
}

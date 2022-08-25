using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridPositionText : MonoBehaviour
{
    object gridObject;
    Unit unit;
    [SerializeField] TextMeshPro text;
    public virtual void SetGridObject(object gridObject)
    {
        this.gridObject = gridObject;
    }
    public virtual void SetText(string text)
    {
        this.text.text = gridObject.ToString();
    }
    protected virtual void Update()
    {
        SetText(gridObject.ToString());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCircle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Unit unit;
    MeshRenderer meshRenderer;
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void Start()
    {
        UnitActionSystem.Instance.OnSelectUnitEvent += HandleSelectUnit;
        // UpdateVisual()
    }
    void HandleSelectUnit(Unit unit)
    {
        UpdateVisual(unit);
    }

    private void UpdateVisual(Unit unit)
    {
        if (unit == this.unit)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }
}

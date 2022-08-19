using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] Unit selectUnit;
    [SerializeField]LayerMask unitLayerMask;
    void Start()
    {

    }
    void Update()
    {
        if(TryHandleUnit())return;
        if (Input.GetKeyDown(KeyCode.T) || Input.GetMouseButtonDown(0))
        {
            selectUnit.SetTarget(MouseController.Instance.GetWorldPosition());
        }
    }
    bool TryHandleUnit()
    {
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hit,float.MaxValue,unitLayerMask))
        {
            if(hit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                selectUnit = unit;
                return true;
            }
        }
        return false;
    }


}

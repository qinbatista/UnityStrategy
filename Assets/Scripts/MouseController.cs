using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public static MouseController Instance {get; private set;}
    [SerializeField]LayerMask interactLayerMask;

    void Start()
    {
        if(Instance==null)Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        transform.position = GetWorldPosition();
    }
    public Vector3 GetWorldPosition()
    {
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hit,float.MaxValue,interactLayerMask))
            return hit.point;
        return Vector3.zero;
    }

}

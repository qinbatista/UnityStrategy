using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualSingle : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    // Start is called before the first frame update
    public void Show(Material material)
    {
        meshRenderer.material = material;
        meshRenderer.enabled = true;
    }
    public void Hide()
    {
        meshRenderer.enabled = false;
    }
}

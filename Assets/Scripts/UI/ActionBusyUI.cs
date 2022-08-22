using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnitActionSystem.Instance.OnBusyChangeEvent+=UpdateBusyVisual;
        Hide();
    }
    void Show()
    {
        gameObject.SetActive(true);
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }
    void UpdateBusyVisual(bool isBusy)
    {
        if (isBusy) Show();
        else Hide();
    }
}

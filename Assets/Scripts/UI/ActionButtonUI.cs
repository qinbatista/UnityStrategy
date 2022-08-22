using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ActionButtonUI : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI textMeshProUGUI;
    [SerializeField]Button button;
    public void  SetBaseAction(BaseAction baseAction)
    {
        textMeshProUGUI.text = baseAction.GetActionName().ToLower();
    }


}

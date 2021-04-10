using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScopeStart : MonoBehaviour, IPointerDownHandler
{
    public ScopeController scopeController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (scopeController.begin)
        {
            scopeController.begin = true;
            txt.text = "Start";
        }

        else if (!scopeController.begin)
        {
            scopeController.begin = false;
            txt.text = "Stop";
        }
    }
}

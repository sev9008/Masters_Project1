using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScopePause : MonoBehaviour, IPointerDownHandler
{
    public ScopeController scopeController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (scopeController.manual)
        {
            scopeController.manual = false;
            txt.text = "Resume";
        }

        else if (!scopeController.manual)
        {
            scopeController.manual = true;
            txt.text = "Pause";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pressed6 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public InputField Text;

    public void OnPointerDown(PointerEventData eventData)
    {
        string txt = Text.text;
        string tmptxt = txt;
        txt = tmptxt + "6";
        Text.text = txt;
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}

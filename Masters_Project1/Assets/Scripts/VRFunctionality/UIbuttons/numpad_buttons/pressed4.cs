using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pressed4 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public InputField Text;

    public void OnPointerDown(PointerEventData eventData)
    {
        string txt = Text.text;
        string tmptxt = txt;
        txt = tmptxt + "4";
        Text.text = txt;
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}

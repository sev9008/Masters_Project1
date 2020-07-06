using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pressedpush : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public InputField Text;
    public int finltext;
    public ArrayKeeper arrayKeeper;

    public void OnPointerDown(PointerEventData eventData)
    {
        string tmptext = Text.text;
        Text.text = "";

        if (int.TryParse(tmptext, out int result))
        {
            finltext = result;
            arrayKeeper.push(finltext);
        }
        else if (!int.TryParse(tmptext, out int result2))
        {
            Text tmp = Text.placeholder.GetComponent<Text>();
            tmp.text = "Must enter a value";
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}

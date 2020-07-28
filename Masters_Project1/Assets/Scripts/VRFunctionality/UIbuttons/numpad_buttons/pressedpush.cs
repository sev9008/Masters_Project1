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
            if (Convert.ToInt32(tmptext) > 100)
            {
                Text tmp = Text.placeholder.GetComponent<Text>();
                tmp.text = "Must enter a value less than 100";
            }
            else if (Convert.ToInt32(tmptext) < 0)
            {
                Text tmp = Text.placeholder.GetComponent<Text>();
                tmp.text = "Must enter a value greater than 100";
            }
            else 
            {
                finltext = result;
                arrayKeeper.push(finltext);
            }
        }
        else if (!int.TryParse(tmptext, out int result3))
        {
            Text tmp = Text.placeholder.GetComponent<Text>();
            tmp.text = "Must enter a value";
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}

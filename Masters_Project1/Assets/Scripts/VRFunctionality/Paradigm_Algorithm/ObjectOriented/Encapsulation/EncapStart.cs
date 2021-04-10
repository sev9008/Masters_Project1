using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EncapStart : MonoBehaviour, IPointerDownHandler
{
    public EncapController encapController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (encapController.begin)
        {
            encapController.begin = false;
            txt.text = "Start";
        }

        else if (!encapController.begin)
        {
            encapController.begin = true;
            txt.text = "Stop";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EncapPause : MonoBehaviour
{
    public EncapController encapController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (encapController.manual)
        {
            encapController.manual = true;
            txt.text = "Resume";
        }

        else if (!encapController.manual)
        {
            encapController.manual = false;
            txt.text = "Pause";
        }
    }
}

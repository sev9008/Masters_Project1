using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PolyMPause : MonoBehaviour, IPointerDownHandler
{
    public PolyMController polyMController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (polyMController.manual)
        {
            polyMController.manual = false;
            txt.text = "Resume";
        }

        else if (!polyMController.manual)
        {
            polyMController.manual = true;
            txt.text = "Pause";
        }
    }
}

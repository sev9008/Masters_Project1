using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PolyMStart : MonoBehaviour, IPointerDownHandler
{
    public PolyMController polyMController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (polyMController.begin)
        {
            polyMController.begin = false;
            txt.text = "Start";
        }

        else if (!polyMController.begin)
        {
            polyMController.begin = true;
            txt.text = "Stop";
        }
    }
}

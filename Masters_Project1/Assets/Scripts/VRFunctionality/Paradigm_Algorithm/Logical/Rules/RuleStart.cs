using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RuleStart : MonoBehaviour, IPointerDownHandler
{
    public RuleController abstractController;
    public Text txt;
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("test");
        if (abstractController.begin)
        {
            abstractController.begin = false;
            txt.text = "Start";
            //Debug.Log("test2");
        }

        else if (!abstractController.begin)
        {
            abstractController.begin = true;
            txt.text = "Stop";
        }
    }
}

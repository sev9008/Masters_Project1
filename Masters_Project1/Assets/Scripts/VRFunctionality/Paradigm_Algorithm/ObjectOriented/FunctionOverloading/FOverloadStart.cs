using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FOverloadStart : MonoBehaviour
{
    public FOverloadController fOverloadController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (fOverloadController.begin)
        {
            fOverloadController.begin = true;
            txt.text = "Start";
        }

        else if (!fOverloadController.begin)
        {
            fOverloadController.begin = false;
            txt.text = "Stop";
        }
    }
}

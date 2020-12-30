using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FOverloadPause : MonoBehaviour
{
    public FOverloadController fOverloadController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (fOverloadController.manual)
        {
            fOverloadController.manual = true;
            txt.text = "Resume";
        }

        else if (!fOverloadController.manual)
        {
            fOverloadController.manual = false;
            txt.text = "Pause";
        }
    }
}

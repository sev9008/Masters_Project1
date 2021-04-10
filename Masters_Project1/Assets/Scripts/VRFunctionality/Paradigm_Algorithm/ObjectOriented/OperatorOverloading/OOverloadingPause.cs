using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OOverloadingPause : MonoBehaviour, IPointerDownHandler
{
    public OOverloadingController oOverloadingController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oOverloadingController.manual)
        {
            oOverloadingController.manual = false;
            txt.text = "Resume";
        }

        else if (!oOverloadingController.manual)
        {
            oOverloadingController.manual = true;
            txt.text = "Pause";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OOverloadingStart : MonoBehaviour
{
    public OOverloadingController oOverloadingController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oOverloadingController.begin)
        {
            oOverloadingController.begin = true;
            txt.text = "Start";
        }

        else if (!oOverloadingController.begin)
        {
            oOverloadingController.begin = false;
            txt.text = "Stop";
        }
    }
}

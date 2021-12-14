using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleCompTut : MonoBehaviour, IPointerDownHandler
{
    public TutorialActiveController tuttogglecontrol;
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("hit2");

        if (tuttogglecontrol.compTut)
        {
            tuttogglecontrol.compTut = false;
        }
        if (!tuttogglecontrol.compTut)
        {
            tuttogglecontrol.compTut = true;
            tuttogglecontrol.algTut = false;
        }
    }
}

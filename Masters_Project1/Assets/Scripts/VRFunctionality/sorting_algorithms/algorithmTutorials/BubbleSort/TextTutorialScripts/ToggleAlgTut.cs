using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleAlgTut : MonoBehaviour, IPointerDownHandler
{
    public TutorialActiveController tuttogglecontrol;
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("hit1");
        if (tuttogglecontrol.algTut)
        {
            tuttogglecontrol.algTut = false;
        }        
        if (!tuttogglecontrol.algTut)
        {
            tuttogglecontrol.algTut = true;
            tuttogglecontrol.compTut = false;
        }
    }
}

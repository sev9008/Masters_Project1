using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_CancelQuit : MonoBehaviour, IPointerDownHandler
{
    public GameObject QuitObj;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (QuitObj.activeInHierarchy)
        {
            QuitObj.SetActive(false);
            
        }
    }
}

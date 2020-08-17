using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerDownHandler
{
    public GameTutorial m_GameTutorial;
    public SelSortGameController m_selSortGameController;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_GameTutorial.enabled = false;
        m_selSortGameController.enabled = true;
    }
}

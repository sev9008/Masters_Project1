using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialButton : MonoBehaviour, IPointerDownHandler
{
    public GameTutorial m_GameTutorial;
    public SelSortGameController m_selSortGameController;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_GameTutorial.enabled = true;
        m_selSortGameController.enabled = false;
    }
}

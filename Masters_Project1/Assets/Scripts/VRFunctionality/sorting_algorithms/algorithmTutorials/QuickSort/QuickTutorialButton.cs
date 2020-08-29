using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickTutorialButton : MonoBehaviour, IPointerDownHandler
{
    public QuickGameTutorial m_GameTutorial;
    public QuickGameController m_InsertGameController;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_GameTutorial.enabled = true;
        m_InsertGameController.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InsertTutorialButton : MonoBehaviour, IPointerDownHandler
{
    public InsertGameTutorial m_GameTutorial;
    public InsertGameController m_InsertGameController;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_GameTutorial.enabled = true;
        m_InsertGameController.enabled = false;
    }
}

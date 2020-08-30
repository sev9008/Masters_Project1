using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BubbleTutorialButton : MonoBehaviour, IPointerDownHandler
{
    public BubbleGameTutorial m_GameTutorial;
    public BubbleGameController m_InsertGameController;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_GameTutorial.enabled = true;
        m_InsertGameController.enabled = false;
    }
}
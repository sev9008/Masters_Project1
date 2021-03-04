using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MergeTutorialButton : MonoBehaviour, IPointerDownHandler
{
    public MergeGameTutorial m_GameTutorial;
    public MergeGameController m_InsertGameController;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_GameTutorial.enabled = true;
        m_InsertGameController.enabled = false;
    }
}

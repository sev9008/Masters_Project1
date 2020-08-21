using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InsertGameStartButton : MonoBehaviour, IPointerDownHandler
{
    public InsertGameTutorial m_GameTutorial;
    public InsertGameController m_InsertGameController;

    private int NumberofGames;
    public void OnPointerDown(PointerEventData eventData)
    {
        m_GameTutorial.enabled = false;
        m_InsertGameController.enabled = false;
        m_InsertGameController.enabled = true;
        NumberofGames++;
        m_InsertGameController.NumOfGamestxt.text = "Number of Games " + NumberofGames;
    }
}
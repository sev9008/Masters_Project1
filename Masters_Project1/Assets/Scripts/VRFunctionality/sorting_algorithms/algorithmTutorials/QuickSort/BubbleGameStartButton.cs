using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BubbleGameStartButton : MonoBehaviour, IPointerDownHandler
{
    public BubbleGameTutorial m_GameTutorial;
    public BubbleGameController m_InsertGameController;

    private int NumberofGames;
    public void OnPointerDown(PointerEventData eventData)
    {
        m_GameTutorial.enabled = false;
        m_InsertGameController.enabled = false;
        m_InsertGameController.enabled = true;
        NumberofGames++;
        m_InsertGameController.numofGamesText.text = "Number of Games " + NumberofGames;
    }
}
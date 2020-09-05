using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MergeGameStartButton : MonoBehaviour, IPointerDownHandler
{
    public MergeGameTutorial m_GameTutorial;
    public MergeGameController m_InsertGameController;

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
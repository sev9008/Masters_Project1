using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_MoveAway : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform Player;
    public GameObject canvas;
    private bool down;

    public void OnPointerDown(PointerEventData eventData)
    {
        down = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        down = false;
    }


    private void Update()
    {
        if (down)
        {
            float step = -1 * Time.deltaTime;
            canvas.transform.position = Vector3.MoveTowards(canvas.transform.position, Player.position, step);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_MoveAway : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform Player;
    public GameObject panel;
    private bool down;
    private void Start()
    {
        GameObject cam = GameObject.FindWithTag("Camera");
        Player = cam.transform;
    }

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
            panel.transform.position = Vector3.MoveTowards(panel.transform.position, Player.position, step);
        }
    }
}

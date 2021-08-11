using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

///the button to start interactive2 tutoiral
public class BubbleInteractive2Start : MonoBehaviour, IPointerDownHandler
{
    public BubbleInteractive2 selsortInteractive2;
    public void OnPointerDown(PointerEventData eventData)
    {
        selsortInteractive2.Begin();
        this.GetComponentInChildren<Text>().text = "Restart";
    }
}
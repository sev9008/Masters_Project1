using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RandomNumbers : MonoBehaviour, IPointerDownHandler
{
    public ArrayKeeper arrayKeeper;

    public void OnPointerDown(PointerEventData eventData)
    {
        arrayKeeper.randomNum();
    }
}

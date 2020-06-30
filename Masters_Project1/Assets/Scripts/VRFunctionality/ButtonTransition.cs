using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Color32 NormalColor = Color.white;
    public Color32 HoverColor = Color.grey;
    public Color32 DownColor = Color.white;

    private Image image = null;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Enter");

        image.color = HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("Exit");

        image.color = NormalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("Down");

        image.color = DownColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print("Up");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("Click");

        image.color = HoverColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MergeInter1_RecursionBlockTEst : MonoBehaviour, IPointerDownHandler
{
    public Material mat;
    public bool pressed;
    public void OnPointerDown(PointerEventData eventData)
    {
        this.gameObject.GetComponentInChildren<MeshRenderer>().material = mat;
        pressed = true;
    }
}
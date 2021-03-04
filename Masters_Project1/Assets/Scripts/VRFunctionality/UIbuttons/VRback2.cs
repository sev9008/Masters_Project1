using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// This script is not currently in use
/// 
/// This script, when pressed, would navigate to the previous UI 
/// Previously the project was set up as navigateable UI pages.  Users would have to navigate down a list to reach different controlelr
/// This of course was annoying and I found it much ebtter to just be able to have different stations for each tutorial.
/// </summary>
public class VRback2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject PanelObject;
    public GameObject spawnowner;
    public GameObject spawnpos;
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 newpos = spawnpos.transform.position;
        PanelObject.transform.position = newpos;
        spawnowner.SetActive(false);
        PanelObject.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}

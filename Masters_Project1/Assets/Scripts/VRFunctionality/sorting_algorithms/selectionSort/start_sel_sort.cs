using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class start_sel_sort : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Sel_sort_CanvasObject;
    public GameObject spawnpos;
    public ArrayKeeper arrayKeeper;
    public GameObject selsortcan;

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 newpos = spawnpos.transform.position;
        if (selsortcan == null)
        {
            selsortcan = Instantiate(Sel_sort_CanvasObject, newpos, spawnpos.transform.rotation);
            selsortcan.GetComponent<selsort_arrayholder>().arr = arrayKeeper.arr;
            selsortcan.GetComponent<selsort_arrayholder>().size = arrayKeeper.size;
        }
        else 
        {
            selsortcan.GetComponent<selsort_arrayholder>().arr = arrayKeeper.arr;
            selsortcan.GetComponent<selsort_arrayholder>().size = arrayKeeper.size;
        }

    }
    public void OnPointerUp(PointerEventData eventData)
    { }
}

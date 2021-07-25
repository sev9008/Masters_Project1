using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseClickInputModuleb : BaseInputModule
{
    public PointerEventData Data { get; private set; } = null;

    public override void Process()
    {
        Data = new PointerEventData(eventSystem);
        Data.position = Input.mousePosition;

        eventSystem.RaycastAll(Data, m_RaycastResultCache);

        //foreach (RaycastResult result in m_RaycastResultCache)
        //{
        //    Debug.Log("Hit " + result.gameObject.name);
        //}

        var temp = FindFirstRaycast(m_RaycastResultCache);

        if (temp.isValid)
        {
            Data.pointerCurrentRaycast = temp;

            HandlePointerExitAndEnter(Data, Data.pointerCurrentRaycast.gameObject);

            ExecuteEvents.Execute(Data.pointerDrag, Data, ExecuteEvents.dragHandler);
        }
    }

    public void Press()
    {
        Data.pointerPressRaycast = Data.pointerCurrentRaycast;

        Data.pointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(Data.pointerPressRaycast.gameObject);
        Data.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(Data.pointerPressRaycast.gameObject);

        ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerDownHandler);
        ExecuteEvents.Execute(Data.pointerDrag, Data, ExecuteEvents.beginDragHandler);
    }

    public void Release()
    {
        GameObject pointerRelease = ExecuteEvents.GetEventHandler<IPointerClickHandler>(Data.pointerCurrentRaycast.gameObject);

        if (Data.pointerPress == pointerRelease)
            ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerClickHandler);

        ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerUpHandler);
        ExecuteEvents.Execute(Data.pointerDrag, Data, ExecuteEvents.endDragHandler);

        Data.pointerPress = null;
        Data.pointerDrag = null;

        Data.pointerCurrentRaycast.Clear();
    }
}

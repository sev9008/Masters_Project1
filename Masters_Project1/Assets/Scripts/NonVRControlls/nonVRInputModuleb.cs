using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// This script utilizes the unity even system to process pressing and releasing on the VR controller
/// </summary>
public class nonVRInputModuleb : BaseInputModule
{
    [SerializeField] private NonVRPointer pointer = null;
    public PointerEventData Data { get; private set; } = null;

    private float timer;

    protected override void Start()
    {
        Data = new PointerEventData(eventSystem);
        Data.position = new Vector2(pointer.Camera.pixelWidth / 2, pointer.Camera.pixelHeight / 2);
    }

    public override void Process()
    {
        //thoguht this would work but it keeps updating the position.  Its kindof wierd.  wish i knew how this behaved mroe. 
        //Data = new PointerEventData(eventSystem);
        //Data.position = new Vector2(pointer.Camera.pixelWidth / 2, pointer.Camera.pixelHeight / 2);

        timer = 0;
        eventSystem.RaycastAll(Data, m_RaycastResultCache);

        var temp = FindFirstRaycast(m_RaycastResultCache);
        if (temp.isValid)
        {
            Data.pointerCurrentRaycast = temp;

            Debug.Log("possible GO" + Data.pointerCurrentRaycast.gameObject);

            HandlePointerExitAndEnter(Data, Data.pointerCurrentRaycast.gameObject);

            ExecuteEvents.Execute(Data.pointerDrag, Data, ExecuteEvents.dragHandler);
        }
        else
        {
            //Debug.Log("nothing found");
        }
    }

    public void Press()
    {
        Data.pointerPressRaycast = Data.pointerCurrentRaycast;

        Debug.Log("GO that was pressed" + Data.pointerPressRaycast.gameObject);
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

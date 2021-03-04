using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// This script is not in use
/// This script attempted co combine the left and right vr controllers into one script.
/// If i remember correctly, two event cameras can not be defined for the same script
/// Thus this script had to be discarded until a better solution can eb found.
/// For now I will continue to use pointer script instead.
/// </summary>
public class PointerV2 : MonoBehaviour
{
    [SerializeField] public float defaultLengthLeft = 7.0f;
    [SerializeField] public float defaultLengthRight = 7.0f;
    [SerializeField] private GameObject Leftdot = null;
    [SerializeField] private GameObject Rightdot = null;
    [SerializeField] public GameObject RightPointer = null;
    [SerializeField] public GameObject LeftPointer = null;

    public Camera Camera { get; private set; } = null;

    private LineRenderer lineRendererLeft = null;
    private LineRenderer lineRendererRight = null;
    private VRInputModule inputModule = null;

    private float colliderDistance;
    private float canvasDistance;

    public RaycastHit hitL;
    public RaycastHit hitR;

    public LayerMask raymask;

    public void Awake()
    {
        Camera = GetComponent<Camera>();
        Camera.enabled = false;

        lineRendererLeft = LeftPointer.GetComponent<LineRenderer>();
        lineRendererRight = RightPointer.GetComponent<LineRenderer>();
    }

    private void Start()
    {
        inputModule = EventSystem.current.gameObject.GetComponent<VRInputModule>();
    }

    private void Update()
    {
        UpdateLineLeft();
        UpdateLineRight();
    }

    private void UpdateLineLeft()
    {
        // Use default or distance
        PointerEventData data = inputModule.Data;
        hitL = CreateRaycastLeft();

        // If nothing is hit, set do default length
        colliderDistance = hitL.distance == 0 ? defaultLengthLeft : hitL.distance;
        canvasDistance = data.pointerCurrentRaycast.distance == 0 ? defaultLengthLeft : data.pointerCurrentRaycast.distance;
        // Get the closest one
        float targetLength = Mathf.Min(colliderDistance, canvasDistance);

        // Default
        Vector3 endPosition = LeftPointer.transform.position + (LeftPointer.transform.forward * targetLength);

        // Set position of the dot
        Leftdot.transform.position = endPosition;

        // Set linerenderer
        lineRendererLeft.SetPosition(0, LeftPointer.transform.position);
        lineRendererLeft.SetPosition(1, endPosition);
    }    
    private void UpdateLineRight()
    {
        // Use default or distance
        PointerEventData data = inputModule.Data;
        hitR = CreateRaycastRight();

        // If nothing is hit, set do default length
        colliderDistance = hitR.distance == 0 ? defaultLengthRight : hitR.distance;
        canvasDistance = data.pointerCurrentRaycast.distance == 0 ? defaultLengthRight : data.pointerCurrentRaycast.distance;
        // Get the closest one
        float targetLength = Mathf.Min(colliderDistance, canvasDistance);

        // Default
        Vector3 endPosition = RightPointer.transform.position + (RightPointer.transform.forward * targetLength);

        // Set position of the dot
        Rightdot.transform.position = endPosition;

        // Set linerenderer
        lineRendererRight.SetPosition(0, RightPointer.transform.position);
        lineRendererRight.SetPosition(1, endPosition);
    }
    private RaycastHit CreateRaycastRight()
    {
        RaycastHit hit;
        Ray ray = new Ray(RightPointer.transform.position, RightPointer.transform.forward);
        Debug.DrawRay(RightPointer.transform.position, RightPointer.transform.forward*7, Color.green);
        Physics.Raycast(ray, out hit, defaultLengthRight);

        return hit;
    }    
    private RaycastHit CreateRaycastLeft()
    {
        RaycastHit hit;
        Ray ray = new Ray(LeftPointer.transform.position, LeftPointer.transform.forward);
        Debug.DrawRay(LeftPointer.transform.position, LeftPointer.transform.forward * 7, Color.green);

        Physics.Raycast(ray, out hit, defaultLengthLeft);

        return hit;
    }
}

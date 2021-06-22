using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NonVRPointer : MonoBehaviour
{
    [SerializeField] public float defaultLength = 7.0f;
    [SerializeField] private GameObject dot = null;

    //public Camera Camera { get; private set; } = null;
    public Camera Camera { get; private set; } = null;

    public LineRenderer lineRenderer = null;
    public VRInputModule inputModule = null;

    private float colliderDistance;
    private float canvasDistance;

    public RaycastHit hit;

    public LayerMask raymask;

    public Vector3 TeleportPos;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
        Camera.enabled = false;

        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        inputModule = EventSystem.current.gameObject.GetComponent<VRInputModule>();

    }

    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        // Use default or distance
        PointerEventData data = inputModule.Data;

        hit = CreateRaycast();

        // If nothing is hit, set do default length

        colliderDistance = hit.distance == 0 ? defaultLength : hit.distance;
        canvasDistance = data.pointerCurrentRaycast.distance == 0 ? defaultLength : data.pointerCurrentRaycast.distance;
        // Get the closest one
        float targetLength = Mathf.Min(colliderDistance, canvasDistance);

        // Default
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        // Set position of the dot
        dot.transform.position = endPosition;

        // Set linerenderer
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength, raymask);
        Debug.DrawRay(transform.position, transform.forward* defaultLength, Color.green);

        //check if raycast hit a wall or any type of blocker
        int tmplayer = -1;
        if (hit.collider != null)
        {
            tmplayer = hit.collider.gameObject.layer;
        }

        if (tmplayer == 12)//if the raycast hit a blocker then its time to change the hit point
        {
            TeleportPos = hit.point - (transform.forward * 3f);
            return hit;
        }
        else
        {
            TeleportPos = hit.point;
            TeleportPos = dot.transform.position;
            return hit;
        }
    }
}

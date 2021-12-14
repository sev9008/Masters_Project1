using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NonVRPointer : MonoBehaviour
{
    [SerializeField] public float defaultLength = 7.0f;
    [SerializeField] private GameObject dot = null;

    //public Camera Camera { get; private set; } = null;
    public Camera Camera;
    //public LineRenderer lineRenderer = null;
    public nonVRInputModule inputModule = null;

    private float colliderDistance;
    private float canvasDistance;

    public RaycastHit hit;

    public LayerMask raymask;


    private void Awake()
    {
        Camera.enabled = false;
    }

    private void Start()
    {
        //inputModule = EventSystem.current.gameObject.GetComponent<nonVRInputModule>();
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

        //Debug.Log("HIT DIST" + hit.distance);
        //Debug.Log("DEF LEN" + defaultLength);

        //this is the one that is screwing up
        //Debug.Log("DATA DIST" + data.pointerCurrentRaycast.distance);

        // If nothing is hit, set do default length
        colliderDistance = hit.distance == 0 ? defaultLength : hit.distance;

        //data.pointerCurrentRaycast.distanc CAUSES AN ERROR.  FOR SOME REASON IT KEEPS GOING FROM 0 TO ACTUAL HIT DISTANCE
        try
        {
            canvasDistance = data.pointerCurrentRaycast.distance == 0 ? defaultLength : data.pointerCurrentRaycast.distance;


            // Get the closest one
            float targetLength = Mathf.Min(colliderDistance, canvasDistance);

            // Default
            Vector3 endPosition = transform.position + (transform.forward * targetLength);

            // Set position of the dot
            dot.transform.position = endPosition;
        }
        catch { Debug.Log("possible error if this log keeps repeating?"); }

        //Debug.Log(hit.collider.gameObject);
    }

    private RaycastHit CreateRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength, raymask);
        //Debug.DrawRay(transform.position, transform.forward* defaultLength, Color.green);


        //check if raycast hit a wall or any type of blocker
        int tmplayer = -1;
        if (hit.collider != null)
        {
            tmplayer = hit.collider.gameObject.layer;
        }

        return hit;
    }
}

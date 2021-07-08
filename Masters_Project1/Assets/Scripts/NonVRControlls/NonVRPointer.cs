﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NonVRPointer : MonoBehaviour
{
    [SerializeField] public float defaultLength = 7.0f;
    [SerializeField] private GameObject dot = null;

    //public Camera Camera { get; private set; } = null;
    public Camera Camera { get; private set; } = null;

    //public LineRenderer lineRenderer = null;
    public nonVRInputModule inputModule = null;

    private float colliderDistance;
    private float canvasDistance;

    public RaycastHit hit;

    public LayerMask raymask;


    private void Awake()
    {
        Camera = GetComponent<Camera>();
        Camera.enabled = false;

        //lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        inputModule = EventSystem.current.gameObject.GetComponent<nonVRInputModule>();
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
        Debug.Log(endPosition);
    }

    private RaycastHit CreateRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength, raymask);
        Debug.DrawRay(transform.position, transform.forward* defaultLength, Color.green);
        return hit;
    }
}

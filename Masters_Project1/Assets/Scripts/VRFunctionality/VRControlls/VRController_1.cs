﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using Valve.VR;

public class VRController_1 : MonoBehaviour
{
    public float Gravity = -10f;
    public float gravitymult = 2f;
    public float sensitivity = 0.1f;
    public float MaxSpeed = 1.0f;
    public float rotateincr = 5f;
    private Vector3 Velocity;


    public SteamVR_Action_Boolean RotatePress = null;
    public SteamVR_Action_Boolean GrabObj = null;
    public SteamVR_Action_Boolean Movepress = null;
    public SteamVR_Action_Vector2 MoveValue = null;

    public SlesortInteractive1 slesortInteractive;
    public InsertSortInteractive1 insertSortInteractive1;
    public QuickSortInteractive1 quickSortInteractive1;
    public BubbleInteractive1 bubbleSortInteractive1;
    public MergeSortInteractive1 mergeSortInteractive1;

    private float Speed = 0.0f;

    private CharacterController CharController = null;
    private Transform CameraRig = null;
    private Transform Head = null;

    public GameObject Spawnpos;
    public GameObject CanvasObject;

    public Pointer m_pointerR;
    public Pointer m_pointerL;

    public GameObject grabbedL;
    public GameObject grabbedR;
    public GameObject DotL;
    public GameObject DotR;

    public bool downR;
    public bool downL;

    private void Awake()
    {
        CharController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        try
        {
            CameraRig = SteamVR_Render.Top().origin;
            Head = SteamVR_Render.Top().head;
        }
        catch { }
    }

    private void Update()
    {
        //HandleHead();
        HandleHieght();
        CalculateMovement();
        SnapRotation();
        HandleMoveObject();

        if (downR)
        {
            m_pointerR.defaultLength = 0f;
            grabbedR.transform.position = m_pointerR.transform.position;
            grabbedR.transform.position = grabbedR.transform.position + (m_pointerR.transform.forward * .3f);
            grabbedR.transform.rotation = new Quaternion(0, 0, 0, 0);
            try 
            {
                grabbedR.GetComponent<BlockParent>().isGrabbed = true;
            }
            catch { }
        }
        if (!downR && grabbedR != null)
        {
            m_pointerR.defaultLength = 7f;
            downR = false;
            try
            {
                grabbedR.GetComponent<BlockParent>().gravity = true;
                GameObject m_parent = grabbedR.GetComponent<BlockParent>().parent;
                grabbedR.transform.parent = m_parent.transform;
                grabbedR.GetComponent<BlockParent>().isGrabbed = false;
            }
            catch { }
            try
            {
                GameObject m_parent = grabbedR.GetComponent<MoveInteractionBLock>().parent;
                grabbedR.transform.parent = m_parent.transform;
            }
            catch { }
            grabbedR = null;
        }        
        if (downL)
        {
            m_pointerL.defaultLength = 0f;
            grabbedL.transform.position = m_pointerL.transform.position;
            grabbedL.transform.position = grabbedL.transform.position + (m_pointerL.transform.forward * .3f);
            grabbedL.transform.rotation = new Quaternion(0, 0, 0, 0);
            try
            {
                grabbedL.GetComponent<BlockParent>().isGrabbed = true;
            }
            catch { }
        }
        if (!downL && grabbedL != null)
        {
            m_pointerL.defaultLength = 7f;
            downL = false;
            try
            {
                grabbedL.GetComponent<BlockParent>().gravity = true;
                GameObject m_parent = grabbedL.GetComponent<BlockParent>().parent;
                grabbedL.transform.parent = m_parent.transform;
                grabbedR.GetComponent<BlockParent>().isGrabbed = false;
            }
            catch { }
            try
            {
                GameObject m_parent = grabbedL.GetComponent<MoveInteractionBLock>().parent;
                grabbedL.transform.parent = m_parent.transform;
            }
            catch { }
            grabbedL = null;
        }
    }

    private void HandleHead()
    {
        //store current head rotation
        Vector3 oldpos = CameraRig.position;
        Quaternion oldrot = CameraRig.rotation;

        //rotation
        transform.eulerAngles = new Vector3(0.0f, Head.rotation.eulerAngles.y, 0.0f);

        //restore
        CameraRig.position = oldpos;
        CameraRig.rotation = oldrot;
    }

    private void CalculateMovement()
    {
        //movement Orientation
        Quaternion orientation = CalculateOrientation();

        Vector3 movement = Vector3.zero;

        //if not moving

        if (MoveValue.axis.magnitude == 0)
        {
            Speed = 0;
        }

        //add clamp
        Speed += MoveValue.axis.magnitude * sensitivity;
        Speed = Mathf.Clamp(Speed, -MaxSpeed, MaxSpeed);

        //orientation and gravity
        movement += orientation * (Speed * Vector3.forward);
        if (!CharController.isGrounded)
        {
            Velocity -= Vector3.up * Gravity * gravitymult * Time.deltaTime; // increases fall gravity for better feel
        }
        else 
        {
            Velocity.y = 0f;
            Debug.Log("Grounded");
        }

        //apply move
        CharController.Move(Velocity * Time.deltaTime);

        CharController.Move(movement * Time.deltaTime);
    }

    private Quaternion CalculateOrientation()
    {
        float rotation = Mathf.Atan2(MoveValue.axis.x, MoveValue.axis.y);
        rotation *= Mathf.Rad2Deg;
        try
        {
            Vector3 orientationEuler = new Vector3(0, Head.eulerAngles.y + rotation, 0);
            return Quaternion.Euler(orientationEuler);
        }
        catch {  }
        Vector3 orientationEuler2 = new Vector3(0, rotation, 0);
        return Quaternion.Euler(orientationEuler2);

    }

    private void HandleHieght()
    {
        //get the head in local space
        try
        {
            float headhieght = Mathf.Clamp(Head.localPosition.y, 1, 2);
            CharController.height = headhieght;
            //cuit in half
            Vector3 newCenter = Vector3.zero;
            newCenter.y = CharController.height / 2;
            newCenter.y += CharController.skinWidth;

            //move capsule in local space
            newCenter.x = Head.localPosition.x;
            newCenter.z = Head.localPosition.z;

            //rotate
            //newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

            //apply
            CharController.center = newCenter;
        }
        catch { }
    }

    private void SnapRotation() 
    {
        float snapValue = 0.0f;

        if (RotatePress.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            snapValue = -Mathf.Abs(rotateincr);
        }

        if (RotatePress.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            snapValue = Mathf.Abs(rotateincr);
        }
        try
        {
            transform.RotateAround(Head.position, Vector3.up, snapValue);
        }
        catch { }
    }

    private void HandleMoveObject()
    {
        try
        {
            if (GrabObj.GetStateDown(SteamVR_Input_Sources.RightHand) && m_pointerR.hit.collider.gameObject.layer == 9)
            {
                m_pointerR.defaultLength = 0f;
                //Debug.Log("down");
                grabbedR = m_pointerR.hit.collider.gameObject;
                grabbedR.transform.parent = DotR.transform;
                try
                {
                    grabbedR.GetComponent<BlockParent>().gravity = false;
                    grabbedR.GetComponent<BlockParent>().isGrabbed = true;
                }
                catch { }
                try 
                {
                    slesortInteractive.arrow.SetActive(true);
                }
                catch { }
                downR = true;
            }
        }
        catch { }
        try
        {
            if (GrabObj.GetStateUp(SteamVR_Input_Sources.RightHand) && grabbedR != null)
            {
                m_pointerR.defaultLength = 7f;
                downR = false;
                try
                {
                    grabbedR.GetComponent<BlockParent>().gravity = true;
                    GameObject m_parent = grabbedR.GetComponent<BlockParent>().parent;
                    grabbedR.transform.parent = m_parent.transform;
                    grabbedR.GetComponent<BlockParent>().isGrabbed = false;
                }
                catch { } 
                try
                {
                    GameObject m_parent = grabbedR.GetComponent<MoveInteractionBLock>().parent;
                    grabbedR.transform.parent = m_parent.transform;
                    slesortInteractive.updatePos();
                    slesortInteractive.arrow.SetActive(false);
                    insertSortInteractive1.updatePos();
                    quickSortInteractive1.updatePos();
                    bubbleSortInteractive1.updatePos();
                    mergeSortInteractive1.updatePos();
                }
                catch { }
                grabbedR = null;
            }
        }
        catch { }        
        
        try
        {
            if (GrabObj.GetStateDown(SteamVR_Input_Sources.LeftHand) && m_pointerL.hit.collider.gameObject.layer == 9)
            {
                m_pointerL.defaultLength= 0f;
                Debug.Log("down");
                grabbedL = m_pointerL.hit.collider.gameObject;
                grabbedL.transform.parent = DotL.transform;
                try
                {
                    grabbedL.GetComponent<BlockParent>().gravity = false;
                    grabbedR.GetComponent<BlockParent>().isGrabbed = true;
                }
                catch { }
                try
                {
                    slesortInteractive.arrow.SetActive(true);
                }
                catch { }
                downL = true;
            }
        }
        catch { }
        try
        {
            if (GrabObj.GetStateUp(SteamVR_Input_Sources.LeftHand) && grabbedL != null)
            {
                m_pointerL.defaultLength = 7f;
                downL = false;
                try
                {
                    grabbedL.GetComponent<BlockParent>().gravity = true;
                    GameObject m_parent = grabbedL.GetComponent<BlockParent>().parent;
                    grabbedL.transform.parent = m_parent.transform;
                    grabbedR.GetComponent<BlockParent>().isGrabbed = false;
                }
                catch { }
                try
                {
                    GameObject m_parent = grabbedL.GetComponent<MoveInteractionBLock>().parent;
                    grabbedL.transform.parent = m_parent.transform;
                    slesortInteractive.updatePos();
                    slesortInteractive.arrow.SetActive(false);
                    insertSortInteractive1.updatePos();
                    quickSortInteractive1.updatePos();
                    bubbleSortInteractive1.updatePos();
                    mergeSortInteractive1.updatePos();
                }
                catch { }
                grabbedL = null;
            }
        }
        catch { }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using Valve.VR;

public class VRController_1 : MonoBehaviour
{
    public float Gravity = 9.8f;
    public float sensitivity = 0.1f;
    public float MaxSpeed = 1.0f;
    public float rotateincr = 5f;

    public SteamVR_Action_Boolean RotatePress = null;
    public SteamVR_Action_Boolean GrabObj = null;
    public SteamVR_Action_Boolean Movepress = null;
    public SteamVR_Action_Vector2 MoveValue = null;

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
        CameraRig = SteamVR_Render.Top().origin;
        Head = SteamVR_Render.Top().head;
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
            grabbedR.GetComponent<BlockParent>().isGrabbed = true;
        }
        if (!downR && grabbedR != null)
        {
            m_pointerR.defaultLength = 7f;
            downR = false;
            grabbedR.GetComponent<BlockParent>().gravity = true;
            GameObject m_parent = grabbedR.GetComponent<BlockParent>().parent;
            grabbedR.transform.parent = m_parent.transform;
            grabbedR = null;
        }        
        if (downL)
        {
            m_pointerL.defaultLength = 0f;
            grabbedL.transform.position = m_pointerL.transform.position;
            grabbedL.transform.position = grabbedL.transform.position + (m_pointerL.transform.forward * .3f);
            grabbedL.transform.rotation = new Quaternion(0, 0, 0, 0);
            grabbedL.GetComponent<BlockParent>().isGrabbed = true;
        }
        if (!downL && grabbedL != null)
        {
            m_pointerL.defaultLength = 7f;
            downL = false;
            grabbedL.GetComponent<BlockParent>().gravity = true;
            GameObject m_parent = grabbedL.GetComponent<BlockParent>().parent;
            grabbedL.transform.parent = m_parent.transform;
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
        movement.y -= Gravity * Time.deltaTime;

        //apply move
        CharController.Move(movement * Time.deltaTime);
    }

    private Quaternion CalculateOrientation()
    {
        float rotation = Mathf.Atan2(MoveValue.axis.x, MoveValue.axis.y);
        rotation *= Mathf.Rad2Deg;

        Vector3 orientationEuler = new Vector3(0, Head.eulerAngles.y + rotation, 0);
        return Quaternion.Euler(orientationEuler);
    }

    private void HandleHieght()
    {
        //get the head in local space
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

        transform.RotateAround(Head.position, Vector3.up, snapValue);
    }

    private void HandleMoveObject()
    {
        try
        {
            if (GrabObj.GetStateDown(SteamVR_Input_Sources.RightHand) && m_pointerR.hit.collider.gameObject.tag == "Moveable")
            {
                m_pointerR.defaultLength = 0f;
                Debug.Log("down");
                grabbedR = m_pointerR.hit.collider.gameObject;
                grabbedR.transform.parent = DotR.transform;
                grabbedR.GetComponent<BlockParent>().gravity = false;
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
                grabbedR.GetComponent<BlockParent>().gravity = true;
                GameObject m_parent = grabbedR.GetComponent<BlockParent>().parent;
                grabbedR.transform.parent = m_parent.transform;
                grabbedR = null;
            }
        }
        catch { }        
        
        try
        {
            if (GrabObj.GetStateDown(SteamVR_Input_Sources.LeftHand) && m_pointerL.hit.collider.gameObject.tag == "Moveable")
            {
                m_pointerL.defaultLength= 0f;
                Debug.Log("down");
                grabbedL = m_pointerL.hit.collider.gameObject;
                grabbedL.transform.parent = DotL.transform;
                grabbedL.GetComponent<BlockParent>().gravity = false;
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
                grabbedL.GetComponent<BlockParent>().gravity = true;
                GameObject m_parent = grabbedL.GetComponent<BlockParent>().parent;
                grabbedL.transform.parent = m_parent.transform;
                grabbedL = null;
            }
        }
        catch { }
    }    
}

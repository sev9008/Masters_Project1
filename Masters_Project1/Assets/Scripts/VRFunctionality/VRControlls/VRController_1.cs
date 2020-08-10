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

    public Pointer m_pointer;

    public GameObject grabbed;
    public GameObject Dot;

    public bool down;

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

        if (down)
        {
            m_pointer.defaultLength = 0f;
            grabbed.transform.position = m_pointer.transform.position;
            grabbed.transform.position = grabbed.transform.position + (m_pointer.transform.forward * .3f);
            grabbed.transform.rotation = new Quaternion(0, 0, 0, 0);
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
            if (GrabObj.GetStateDown(SteamVR_Input_Sources.RightHand) && m_pointer.hit.collider.gameObject.tag == "Moveable")
            {
                Debug.Log("down");
                grabbed = m_pointer.hit.collider.gameObject;
                grabbed.transform.parent = Dot.transform;
                grabbed.GetComponent<BlockParent>().gravity = false;
                down = true;
            }
        }
        catch { }
        try
        {
            if (GrabObj.GetStateUp(SteamVR_Input_Sources.RightHand) && grabbed != null)
            {
                m_pointer.defaultLength = 7f;
                down = false;
                grabbed.GetComponent<BlockParent>().gravity = true;
                GameObject m_parent = grabbed.GetComponent<BlockParent>().parent;
                grabbed.transform.parent = m_parent.transform;
                grabbed = null;

            }
        }
        catch { }
    }    
}

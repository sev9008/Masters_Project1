using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class NonVRControlls : MonoBehaviour
{
    public float Gravity = -10f;
    public float gravitymult = 2f;
    public float sensitivity = 0.1f;
    public float MaxSpeed = 1.0f;
    public float rotateincr = 5f;

    public SlesortInteractive1 slesortInteractive;
    public InsertSortInteractive1 insertSortInteractive1;
    public QuickSortInteractive1 quickSortInteractive1;
    public BubbleInteractive1 bubbleSortInteractive1;
    public MergeSortInteractive1 mergeSortInteractive1;

    public NonVRPointer m_pointer;
    public GameObject pointobj;

    public GameObject grabbed;
    public GameObject Dot;

    public bool down;
    public bool ActiveHighlight;

    public DotHighlightTip dotHighlightTip;

    public GameObject QuitOBj;
    public Camera cam;
    CharacterController charController;
    bool Grounded;
    Vector3 Velocity;
    float GroundDistance = 0.4f;
    public float speed = 10f;
    public LayerMask GroundMask;
    public Transform GroundCheck;
    public float MoveSpeed = 5;

    public bool Quitmode = false;

    public RaycastHit hit;

    public LayerMask raymask;

    public nonVRInputModule nonVRInputModule;
    public MouteInputModule mouteInputModule;
    public float objdistance;

    private void Awake()
    {
        cam = Camera.main;
    }
    private void Start()
    {
        XRSettings.LoadDeviceByName("");
        XRSettings.enabled = false;
        Debug.Log("XRDisabled");

        charController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (QuitOBj.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            pointobj.SetActive(false);
            nonVRInputModule.enabled = false;
            mouteInputModule.enabled = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            pointobj.SetActive(true);
            nonVRInputModule.enabled = true;
            mouteInputModule.enabled = false;
        }

        HandleMoveObject();
        HilightController();
        quitApplciation();

        //if the player has grabbed an object:
        //set the grabbed object's parent to the controller
        //set the length of the controller line to 0.  That way it will naturally disappear.
        //then set the grabbed item 3f in front of the controller.
        //THis looks much better, and is easier to controll.
        if (down)
        {
            m_pointer.defaultLength = 0f;

            grabbed.transform.position = m_pointer.transform.position;
            grabbed.transform.position = grabbed.transform.position + (m_pointer.transform.forward * 1);
            grabbed.transform.rotation = new Quaternion(0, 0, 0, 0);
            try
            {
                grabbed.GetComponent<BlockParent>().isGrabbed = true;
            }
            catch { }
        }
        //if the player has released the object:
        //reset the objects parent
        //reset the length of the controller
        if (!down && grabbed != null)
        {
            m_pointer.defaultLength = 7f;
            down = false;
            try//use this for BlockParent script which utilizes gravity
            {
                grabbed.GetComponent<BlockParent>().gravity = true;
                GameObject m_parent = grabbed.GetComponent<BlockParent>().parent;
                grabbed.transform.parent = m_parent.transform;
                grabbed.GetComponent<BlockParent>().isGrabbed = false;
            }
            catch { }
            try//use this for MoveInteractionBLock script which does not utilize a rigid body.
            {
                GameObject m_parent = grabbed.GetComponent<MoveInteractionBLock>().parent;
                grabbed.transform.parent = m_parent.transform;
            }
            catch { }
            grabbed = null;
        }

        Grounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask, QueryTriggerInteraction.Ignore);
        if (!Grounded)
        {
            Grounded = charController.isGrounded;
        }
        Movement();
    }

    void Movement()
    {
        if (!Grounded)
        {
            Velocity += Vector3.up * Gravity * Time.deltaTime; // increases fall gravity for better feel
        }
        else
        {
            Velocity = Vector3.up * 0;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //transform.right and transform.forward uses local coords instead of world coords
        Vector3 move = transform.right * x + transform.forward * z;

        //Debug.Log(Velocity);
        charController.Move(Velocity * Time.deltaTime);
        charController.Move(move * MoveSpeed * Time.deltaTime);
    }


    //handles object grabbing, novement, and releasing
    private void HandleMoveObject()//if an object is moveable remember to change its layer
    {
        try
        {
            if (Input.GetButtonDown("Fire1") && m_pointer.hit.collider.gameObject.layer == 9)
            {
                
                m_pointer.defaultLength = 0f;
                grabbed = m_pointer.hit.collider.gameObject;
                grabbed.transform.parent = Dot.transform;

                //objdistance = Vector3.Distance(transform.position, grabbed.transform.position);

                try
                {
                    grabbed.GetComponent<BlockParent>().gravity = false;
                    grabbed.GetComponent<BlockParent>().isGrabbed = true;
                }
                catch { }
                try
                {
                    //slesortInteractive.arrow.SetActive(true);
                }
                catch { }
                down = true;
            }
        }
        catch { }
        try
        {
            if (Input.GetButtonUp("Fire1") && grabbed != null)
            {
                m_pointer.defaultLength = 7f;
                down = false;
                try
                {
                    grabbed.GetComponent<BlockParent>().gravity = true;
                    GameObject m_parent = grabbed.GetComponent<BlockParent>().parent;
                    grabbed.transform.parent = m_parent.transform;
                    grabbed.GetComponent<BlockParent>().isGrabbed = false;
                }
                catch { }
                //will reset the position of MoveInteractionBLock
                try
                {
                    GameObject m_parent = grabbed.GetComponent<MoveInteractionBLock>().parent;
                    grabbed.transform.parent = m_parent.transform;
                    slesortInteractive.updatePos();
                    //slesortInteractive.arrow.SetActive(false);
                    insertSortInteractive1.updatePos();
                    quickSortInteractive1.updatePos();
                    bubbleSortInteractive1.updatePos();
                    mergeSortInteractive1.updatePos();
                }
                catch { }
                grabbed = null;
            }
        }
        catch { }
    }

    /// <summary>
    /// this function controlls whether the tip higlighter is active or not.  it it mainly just a toggle button.  
    /// It interacts with test highlight that should be found at the top level of all tutorials, and DotHighlightTip which is found on the Dot connected to teh cotnroller pointer
    /// </summary>
    private void HilightController()
    {
        if (Input.GetButtonDown("Fire2") && dotHighlightTip.TipIsActive)//turn off highlight
        {
            Debug.Log("off");
            ActiveHighlight = false;
        }
        else if (Input.GetButtonDown("Fire2") && !dotHighlightTip.TipIsActive)//turn on highlight
        {
            Debug.Log("on");
            ActiveHighlight = true;
        }

        if (ActiveHighlight)//if true turn on
        {
            dotHighlightTip.TipIsActive = true;
        }
        else
        {
            dotHighlightTip.TipIsActive = false;
        }
    }


    private void quitApplciation()
    {
        if (Input.GetButtonDown("Quit") && !QuitOBj.activeInHierarchy)
        {
            QuitOBj.SetActive(true);
        }        
        else if (Input.GetButtonDown("Quit") && QuitOBj.activeInHierarchy)
        {
            QuitOBj.SetActive(false);
        }
    }
}

//    private void quitApplciation()
//    { 
//        if(Input.GetButtonDown("Quit"))
//        {
//            QuitOBj.SetActive(true);
//            StartCoroutine(waitForKeyPress());
//        }
//    }

//    private IEnumerator waitForKeyPress()
//    {
//        bool done = false;
//        while (!done) // essentially a "while true", but with a bool to break out naturally
//        {
//            if (Input.GetButtonUp("Quit"))
//            {
//                done = true;
//            }
//            yield return null; // wait until next frame, then continue execution from here (loop continues)
//        }


//        done = false;
//        while (!done) // essentially a "while true", but with a bool to break out naturally
//        {
//            if (Input.GetButtonDown("Quit"))
//            {
//                Application.Quit();
//            }
//            else if (Input.GetButtonDown("CancelQuit"))
//            {
//                QuitOBj.SetActive(false);
//                done = true; // breaks the loop
//            }
//            yield return null; // wait until next frame, then continue execution from here (loop continues)
//        }
//    }
//

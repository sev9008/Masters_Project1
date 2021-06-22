using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public Pointer m_pointer;

    public GameObject grabbed;
    public GameObject Dot;

    public bool down;
    public bool ActiveHighlight;

    public Pointer PointerR;
    public DotHighlightTip dotHighlightTip;

    private void Start()
    {
        XRSettings.LoadDeviceByName("");
        XRSettings.enabled = false;
        Debug.Log("XRDisabled");
    }
    private void Update()
    {
        HandleMoveObject();
        HilightController();

        //if the player has grabbed an object:
        //set the grabbed object's parent to the controller
        //set the length of the controller line to 0.  That way it will naturally disappear.
        //then set the grabbed item 3f in front of the controller.
        //THis looks much better, and is easier to controll.
        if (down)
        {
            m_pointer.defaultLength = 0f;
            grabbed.transform.position = m_pointer.transform.position;
            grabbed.transform.position = grabbed.transform.position + (m_pointer.transform.forward * 1.5f);
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
}

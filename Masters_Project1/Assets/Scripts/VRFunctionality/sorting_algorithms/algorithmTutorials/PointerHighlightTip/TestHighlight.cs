using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHighlight : MonoBehaviour
{
    public GameObject RDot;
    private DotHighlightTip dotHighlightTip;
    public string TutorialInformation;
    public float distance = 1f;
    private void Start()
    {
        dotHighlightTip = RDot.GetComponent<DotHighlightTip>();
    }
    /// <summary>
    /// if the dot is in range of a tip then the tip text will be change to that tip's text
    /// </summary>
    private void Update()
    {
        if (Vector3.Distance(RDot.transform.position, transform.position) < distance)
        {
            dotHighlightTip.InRangeTut = true;
            Debug.Log("change text");
            dotHighlightTip.text.text = TutorialInformation;
            //change the text of the highlight here
        }
        else
        {
            dotHighlightTip.InRangeTut = false;
        }
    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, distance);
    }
}

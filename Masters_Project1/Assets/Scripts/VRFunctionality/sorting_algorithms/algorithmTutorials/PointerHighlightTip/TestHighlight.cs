using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHighlight : MonoBehaviour
{
    public GameObject RDot;
    private DotHighlightTip dotHighlightTip;
    public string TutorialInformation;

    private void Start()
    {
        dotHighlightTip = RDot.GetComponent<DotHighlightTip>();
    }
    /// <summary>
    /// if the dot is in range of a tip then the tip text will be change to that tip's text
    /// </summary>
    private void Update()
    {
        if (Vector3.Distance(RDot.transform.position, transform.position) < 1)
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
}

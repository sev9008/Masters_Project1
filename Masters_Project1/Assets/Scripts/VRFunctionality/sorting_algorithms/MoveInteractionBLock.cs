using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveInteractionBLock : MonoBehaviour
{
    public GameObject PairedPos;

    public GameObject parent;
    //public SlesortInteractive1 slesortInteractive;
    //public bool hit;

    bool correctvalue;

    //private void OnTriggerEnter(Collider other)
    //{
    //    int tempnum, tempnum2;
    //    int.TryParse(other.gameObject.tag, out tempnum);
    //    int.TryParse(this.gameObject.tag, out tempnum2);

    //    if (tempnum > 0 && tempnum < 9)
    //    {
    //        Debug.Log("hit");

    //        float.TryParse(this.GetComponentInChildren<Text>().text, out float thisvalue);
    //        if (thisvalue == slesortInteractive.currentSmallest)
    //        {
    //            correctvalue = true;
    //            slesortInteractive.SwapValues(tempnum2 - 1, tempnum - 1);
    //            slesortInteractive.updatePos();
    //        }
    //        else 
    //        {
    //            slesortInteractive.Step.text = "Incorrect.  The block you attempted to swap was not the smallest value in the unsorted array.";
    //            slesortInteractive.incorretAnswers += 1;
    //            slesortInteractive.incorretAnswersText.GetComponent<Text>().text = slesortInteractive.incorretAnswers.ToString();
    //            slesortInteractive.updatePos();
    //        }
    //        slesortInteractive.arrow.SetActive(false);
    //    }
    //}
}

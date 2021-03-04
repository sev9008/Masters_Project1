using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotHighlightTip : MonoBehaviour
{
    public bool TipIsActive;
    public GameObject TipObj;
    public GameObject DeacObj;
    public bool InRangeTut;
    public Text text;
    private void Start()
    {
        TipIsActive = false;
    }

    /// <summary>
    /// if Dot is in range of a tutorial it will display that tutorials tip text.
    /// otherwise it will either display nothing or deactivate.
    /// </summary>
    private void Update()
    {
        if (TipIsActive && !TipObj.activeInHierarchy && InRangeTut)
        {
            TipObj.SetActive(true);
            DeacObj.SetActive(false);
        }
        else if (TipIsActive && !TipObj.activeInHierarchy && !InRangeTut)
        {
            TipObj.SetActive(true);
            DeacObj.SetActive(false);
            text.text = "No tutorial in range";
        }
        else if (!TipIsActive && TipObj.activeInHierarchy)
        {
            TipObj.SetActive(false);
            DeacObj.SetActive(true);
        }
    }
}

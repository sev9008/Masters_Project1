using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyMController : MonoBehaviour
{
    public List<GameObject> Panels;
    public List<GameObject> Output;
    public bool begin;
    public bool manual;
    private bool paused;
    private Coroutine co;
    private void Start()//deactivate all uneeded panels
    {
        for (int i = 0; i < Panels.Count; i++)
        {
            Panels[i].SetActive(false);
        }
        for (int i = 0; i < Output.Count; i++)
        {
            Output[i].SetActive(false);
        }
    }
    void Update()
    {
        if (begin && co == null)//start the coroutine
        {
            co = StartCoroutine(ScopeRun());
        }
        if (!begin && co != null)//stop the coroutine and set all panels to deactivated
        {
            StopCoroutine(co);
            co = null;
            for (int i = 0; i < Panels.Count; i++)
            {
                Panels[i].SetActive(false);
            }
            for (int i = 0; i < Output.Count; i++)
            {
                Output[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// Coroutine that will run through all panels and dispaly the correct ones in sequence
    /// </summary>
    public IEnumerator ScopeRun()
    {
        while (begin)//repeat the routine as long as begin is true
        {
            Start();
            for (int i = 0; i < Panels.Count; i++)//run through every panel
            {
                if (i > 0)//disable the previous panel
                {
                    Panels[i - 1].SetActive(false);
                }

                if (i == 8)//enable or disable the first output
                {
                    Output[0].SetActive(true);
                }
                else
                {
                    Output[0].SetActive(false);
                }

                if (i == 11)//enable or disable the first output
                {
                    Output[1].SetActive(true);
                }
                else
                {
                    Output[1].SetActive(false);
                }

                Panels[i].SetActive(true);//activate the current panel

                //next two case statements control how the algorithm waits for the next move
                if (!manual)//pause the routine for a second
                {
                    yield return new WaitForSeconds(1);
                }
                if (manual)
                {
                    paused = true;
                }
                while (paused && manual)//this case statement uses two bools.  it does this in case i want to add a manual control alter
                {
                    yield return null;
                    if (!manual)
                    {
                        paused = false;
                    }
                }
            }
        }
    }
}

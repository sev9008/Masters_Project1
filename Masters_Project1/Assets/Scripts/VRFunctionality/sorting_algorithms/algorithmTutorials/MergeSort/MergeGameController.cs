using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeGameController : MonoBehaviour
{
    public GameObject[] b;
    public GameObject[] bclone;
    public GameObject[] pos;
    public Material Donesort;
    public Material Nextsort;
    public Material Unsorted;
    public Material nextLine;

    public bool sorted;

    public Text Step;

    public VRController_1 m_vRController_1;

    Coroutine co;
    Coroutine sho;

    public int speed;
    public int smooth;

    private Vector2 targetTransform;
    private Vector2 targetTransform2;

    public int currentSmallestIndex;
    public int nextToSort;
    public float dist1;

    private int corretAnswers;
    private int incorretAnswers;
    private int numofGames;
    public Text corretAnswersText;
    public Text incorretAnswersText;
    public Text numofGamesText;

    public bool waitingforswap;

    public GameObject[] L;
    public GameObject[] R;
    public bool Larray;

    private void Start()
    {
        corretAnswersText.GetComponentInChildren<Text>().text = "0";
        incorretAnswersText.GetComponentInChildren<Text>().text = "0";
        numofGamesText.GetComponentInChildren<Text>().text = "0";
        incorretAnswers = 0;
        corretAnswers = 0;
        numofGames = 0;
        //Begin();
    }
    private void OnEnable()
    {
        Begin();
    }
    public void Begin()
    {
        numofGames += 1;
        numofGamesText.GetComponent<Text>().text = numofGames.ToString();
        Step.text = "Welcome!  This interactive minigame is designed to teach you how to perform Merge Sort." + "\nIf a block is green It must be swapped with the another green block.";
        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
            b[i].GetComponent<BlockParent>().PairedPos = pos[i];
            b[i].GetComponent<BlockParent>().gravity = true;
            bclone[i].GetComponentInChildren<Text>().text = b[i].GetComponentInChildren<Text>().text;
        }
        updatePos();
        sorted = false;
        StartCoroutine(Mergechecksort());
    }
    public void updatePos()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            b[i].transform.position = pos[i].transform.position;
            b[i].transform.rotation = pos[i].transform.rotation;
            b[i].GetComponent<BlockParent>().PairedPos = pos[i];
        }
    }

    public void Update()
    {
        if (sorted)
        {
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Merge Sort Operates." + "\nThe Algorithm splits the array into halves and sorts each half one at a time.";
            sorted = false;
        }

        if (waitingforswap && ((L.Length > 0 && Larray) || (R.Length > 0 && !Larray)))
        {
            //Debug.Log(b.Length + "  " + R.Length + "  " + L.Length);
            //Debug.Log(currentSmallestIndex + "  " + nextToSort);
            //Debug.Log(Larray);
            float dist2;
            if (Larray)
            {
                dist1 = Vector3.Distance(b[nextToSort].transform.position, L[currentSmallestIndex].GetComponent<BlockParent>().PairedPos.transform.position);
                dist2 = Vector3.Distance(L[currentSmallestIndex].transform.position, b[nextToSort].GetComponent<BlockParent>().PairedPos.transform.position);
                if ((dist1 < .5 && dist1 != 0) || (dist2 < .5 && dist2 != 0))
                {
                    Debug.Log("Swap");
                    if (m_vRController_1.grabbedL != null)
                    {
                        m_vRController_1.grabbedL.GetComponent<BlockParent>().isGrabbed = false;
                    }
                    if (m_vRController_1.grabbedR != null)
                    {
                        m_vRController_1.grabbedR.GetComponent<BlockParent>().isGrabbed = false;
                    }
                    m_vRController_1.downR = false;
                    m_vRController_1.downL = false;
                    corretAnswers++;
                    corretAnswersText.text = "Correct Anwserws = " + corretAnswers;
                    waitingforswap = false;
                    updatePos();
                    return;
                }
                else
                {
                    for (int i = 0; i < b.Length; i++)
                    {
                        dist1 = Vector3.Distance(b[i].transform.position, L[currentSmallestIndex].GetComponent<BlockParent>().PairedPos.transform.position);
                        dist2 = Vector3.Distance(L[currentSmallestIndex].transform.position, b[i].GetComponent<BlockParent>().PairedPos.transform.position);
                        if ((dist1 < .5 && dist1 != 0) || (dist2 < .5 && dist2 != 0))
                        {
                            if (m_vRController_1.grabbedL != null)
                            {
                                m_vRController_1.grabbedL.GetComponent<BlockParent>().isGrabbed = false;
                            }
                            if (m_vRController_1.grabbedR != null)
                            {
                                m_vRController_1.grabbedR.GetComponent<BlockParent>().isGrabbed = false;
                            }
                            m_vRController_1.downR = false;
                            m_vRController_1.downL = false;
                            Step.text = "Incorrect.  The block you attempted to swap was incorrect.";
                            incorretAnswers++;
                            incorretAnswersText.text = "Incorrect Anwserws = " + incorretAnswers;
                            updatePos();
                            return;
                        }

                        dist1 = Vector3.Distance(b[i].transform.position, b[nextToSort].GetComponent<BlockParent>().PairedPos.transform.position);
                        dist2 = Vector3.Distance(b[nextToSort].transform.position, b[i].GetComponent<BlockParent>().PairedPos.transform.position);
                        if ((dist1 < .5 && dist1 != 0) || (dist2 < .5 && dist2 != 0))
                        {
                            if (m_vRController_1.grabbedL != null)
                            {
                                m_vRController_1.grabbedL.GetComponent<BlockParent>().isGrabbed = false;
                            }
                            if (m_vRController_1.grabbedR != null)
                            {
                                m_vRController_1.grabbedR.GetComponent<BlockParent>().isGrabbed = false;
                            }
                            m_vRController_1.downR = false;
                            m_vRController_1.downL = false;
                            Step.text = "Incorrect.  The block you attempted to swap was incorrect.";
                            incorretAnswers++;
                            incorretAnswersText.text = "Incorrect Anwserws = " + incorretAnswers;
                            updatePos();
                            return;
                        }
                    }
                }
            }
            else
            {
                dist1 = Vector3.Distance(b[nextToSort].transform.position, R[currentSmallestIndex].GetComponent<BlockParent>().PairedPos.transform.position);
                dist2 = Vector3.Distance(R[currentSmallestIndex].transform.position, b[nextToSort].GetComponent<BlockParent>().PairedPos.transform.position);
                if ((dist1 < .5 && dist1 != 0) || (dist2 < .5 && dist2 != 0))
                {
                    if (m_vRController_1.grabbedL != null)
                    {
                        m_vRController_1.grabbedL.GetComponent<BlockParent>().isGrabbed = false;
                    }
                    if (m_vRController_1.grabbedR != null)
                    {
                        m_vRController_1.grabbedR.GetComponent<BlockParent>().isGrabbed = false;
                    }
                    Debug.Log("Swap");
                    m_vRController_1.downR = false;
                    m_vRController_1.downL = false;
                    corretAnswers++;
                    corretAnswersText.text = "Correct Anwserws = " + corretAnswers;
                    waitingforswap = false;
                    updatePos();
                    return;
                }
                else
                {
                    for (int i = 0; i < b.Length; i++)
                    {
                        dist1 = Vector3.Distance(b[i].transform.position, L[currentSmallestIndex].GetComponent<BlockParent>().PairedPos.transform.position);
                        dist2 = Vector3.Distance(R[currentSmallestIndex].transform.position, b[i].GetComponent<BlockParent>().PairedPos.transform.position);
                        if ((dist1 < .5 && dist1 != 0) || (dist2 < .5 && dist2 != 0))
                        {
                            if (m_vRController_1.grabbedL != null)
                            {
                                m_vRController_1.grabbedL.GetComponent<BlockParent>().isGrabbed = false;
                            }
                            if (m_vRController_1.grabbedR != null)
                            {
                                m_vRController_1.grabbedR.GetComponent<BlockParent>().isGrabbed = false;
                            }
                            m_vRController_1.downR = false;
                            m_vRController_1.downL = false;
                            Step.text = "Incorrect.  The block you attempted to swap was incorrect.";
                            incorretAnswers++;
                            incorretAnswersText.text = "Incorrect Anwserws = " + incorretAnswers;
                            updatePos();
                            return;
                        }

                        dist1 = Vector3.Distance(b[i].transform.position, b[nextToSort].GetComponent<BlockParent>().PairedPos.transform.position);
                        dist2 = Vector3.Distance(b[nextToSort].transform.position, b[i].GetComponent<BlockParent>().PairedPos.transform.position);
                        if ((dist1 < .5 && dist1 != 0) || (dist2 < .5 && dist2 != 0))
                        {
                            if (m_vRController_1.grabbedL != null)
                            {
                                m_vRController_1.grabbedL.GetComponent<BlockParent>().isGrabbed = false;
                            }
                            if (m_vRController_1.grabbedR != null)
                            {
                                m_vRController_1.grabbedR.GetComponent<BlockParent>().isGrabbed = false;
                            }
                            m_vRController_1.downR = false;
                            m_vRController_1.downL = false;
                            Step.text = "Incorrect.  The block you attempted to swap was incorrect.";
                            incorretAnswers++;
                            incorretAnswersText.text = "Incorrect Anwserws = " + incorretAnswers;
                            updatePos();
                            return;
                        }
                    }
                }
            }
            //{
            //    dist1 = Vector3.Distance(b[nextToSort].transform.position, R[currentSmallestIndex].transform.position);
            //    if (dist1 < .04)
            //    {
            //        Debug.Log("Swap");
            //        m_vRController_1.downR = false;
            //        m_vRController_1.downL = false;
            //        corretAnswers++;
            //        corretAnswersText.text = "Correct Anwserws = " + corretAnswers;
            //        waitingforswap = false;
            //        updatePos();
            //        return;
            //    }
            //    else
            //    {
            //        for (int i = 0; i < b.Length; i++)
            //        {
            //            for (int j = 0; j < b.Length; j++)
            //            {
            //                dist1 = Vector3.Distance(b[i].transform.position, R[currentSmallestIndex].transform.position);
            //                dist2 = Vector3.Distance(b[nextToSort].transform.position, b[j].transform.position);
            //                if (dist1 < .04 && dist1 != 0)
            //                {
            //                    m_vRController_1.downR = false;
            //                    m_vRController_1.downL = false;
            //                    Step.text = "Incorrect.  The block you attempted to swap was incorrect.";
            //                    incorretAnswers++;
            //                    incorretAnswersText.text = "Incorrect Anwserws = " + incorretAnswers;
            //                    updatePos();
            //                    return;
            //                }
            //                if (dist2 < .04 && dist2 != 0)
            //                {
            //                    m_vRController_1.downR = false;
            //                    m_vRController_1.downL = false;
            //                    Step.text = "Incorrect.  The block you attempted to swap was incorrect.";
            //                    incorretAnswers++;
            //                    incorretAnswersText.text = "Incorrect Anwserws = " + incorretAnswers;
            //                    updatePos();
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //}
        }
        //for (int i = 0; i < b.Length; i++)
        //{
        //    float dist1 = Vector3.Distance(b[i].transform.position, b[i].GetComponent<BlockParent>().PairedPos.transform.position);

        //    if (dist1 > 20)
        //    {
        //        if (m_vRController_1.grabbedL != null)
        //        {
        //            m_vRController_1.grabbedL.GetComponent<BlockParent>().isGrabbed = false;
        //        }
        //        if (m_vRController_1.grabbedR != null)
        //        {
        //            m_vRController_1.grabbedR.GetComponent<BlockParent>().isGrabbed = false;
        //        }
        //        m_vRController_1.downR = false;
        //        m_vRController_1.downL = false;
        //        b[i].GetComponent<BlockParent>().isGrabbed = false;


        //        pos[i].GetComponent<BoxCollider>().enabled = true;
        //        updatePos();
        //    }
        //}
    }

    public IEnumerator Mergechecksort()
    {
        yield return StartCoroutine(mergeSort(0, b.Length - 1));
        updatePos();
        sorted = true;
    }

    public IEnumerator mergeSort(int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;
            yield return mergeSort(l, m);
            yield return mergeSort(m + 1, r);
            yield return merge(l, m, r);
        }
    }

    public IEnumerator merge(int l, int m, int r)
    {
        EnableTrigger(l, r);
        float temp, temp2;
        int i, j, k;
        int n1 = m - l + 1;
        int n2 = r - m;
        L = new GameObject[n1];
        R = new GameObject[n2];
        for (i = 0; i < n1; i++)
        {
            L[i] = b[l + i];
        }
        for (j = 0; j < n2; j++)
        {
            R[j] = b[m + 1 + j];
        }
        i = 0;
        j = 0;
        k = l;
        yield return new WaitForSeconds(speed);
        while (i < n1 && j < n2)
        {
            float.TryParse(L[i].GetComponentInChildren<Text>().text, out temp);
            float.TryParse(R[j].GetComponentInChildren<Text>().text, out temp2);

            if (temp <= temp2)
            {
                nextToSort = k;
                currentSmallestIndex = i;

                b[k].GetComponent<BlockParent>().enabled = true;
                b[k].GetComponent<BoxCollider>().enabled = true;
                b[k].GetComponent<Rigidbody>().isKinematic = false;
                pos[k].GetComponent<BoxCollider>().enabled = false;
                //pos[k].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                pos[k].GetComponentInChildren<MeshRenderer>().enabled = true;
                b[k].GetComponent<BlockParent>().gravity = true;

                L[i].GetComponent<BlockParent>().enabled = true;
                L[i].GetComponent<BoxCollider>().enabled = true;
                L[i].GetComponent<Rigidbody>().isKinematic = false;
                L[i].GetComponent<BlockParent>().PairedPos.GetComponent<BoxCollider>().enabled = false;
                //L[i].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Nextsort;
                L[i].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().enabled = true;
                L[i].GetComponent<BlockParent>().gravity = true;

                Larray = true;
                waitingforswap = true;

                float.TryParse(b[k].GetComponentInChildren<Text>().text, out temp);
                float.TryParse(L[i].GetComponentInChildren<Text>().text, out temp2);
                if (temp == temp2)
                {
                    waitingforswap = false;
                }
                while (waitingforswap)
                {
                    yield return null;
                }
                b[k].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Donesort;
                L[i].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Donesort;

                bclone[k].SetActive(true);
                bclone[k].GetComponentInChildren<Text>().text = L[i].GetComponentInChildren<Text>().text;

                i++;
            }
            else if (temp != temp2)
            {
                nextToSort = k;
                currentSmallestIndex = j;

                b[k].GetComponent<BlockParent>().enabled = true;
                b[k].GetComponent<BoxCollider>().enabled = true;
                b[k].GetComponent<Rigidbody>().isKinematic = false;
                pos[k].GetComponent<BoxCollider>().enabled = false;
                //pos[k].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                pos[k].GetComponentInChildren<MeshRenderer>().enabled = true;
                b[k].GetComponent<BlockParent>().gravity = true;

                R[j].GetComponent<BlockParent>().enabled = true;
                R[j].GetComponent<BoxCollider>().enabled = true;
                R[j].GetComponent<Rigidbody>().isKinematic = false;
                R[j].GetComponent<BlockParent>().PairedPos.GetComponent<BoxCollider>().enabled = false;
                //R[j].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Nextsort;
                R[j].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().enabled = true;
                R[j].GetComponent<BlockParent>().gravity = true;

                Larray = false;
                waitingforswap = true;

                float.TryParse(b[k].GetComponentInChildren<Text>().text, out temp);
                float.TryParse(R[j].GetComponentInChildren<Text>().text, out temp2);
                if (temp == temp2)
                {
                    waitingforswap = false;
                }
                while (waitingforswap)
                {
                    yield return null;
                }
                b[k].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Donesort;
                R[j].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Donesort;

                bclone[k].SetActive(true);
                bclone[k].GetComponentInChildren<Text>().text = R[j].GetComponentInChildren<Text>().text;

                j++;
            }
            EnableTrigger(l, r);
            updatePos();
            k++;
        }
        while (i < n1)
        {
            nextToSort = k;
            currentSmallestIndex = i;

            b[k].GetComponent<BlockParent>().enabled = true;
            b[k].GetComponent<BoxCollider>().enabled = true;
            b[k].GetComponent<Rigidbody>().isKinematic = false;
            pos[k].GetComponent<BoxCollider>().enabled = false;
            //pos[k].GetComponentInChildren<MeshRenderer>().material = Nextsort;
            pos[k].GetComponentInChildren<MeshRenderer>().enabled = true;
            b[k].GetComponent<BlockParent>().gravity = true;

            L[i].GetComponent<BlockParent>().enabled = true;
            L[i].GetComponent<BoxCollider>().enabled = true;
            L[i].GetComponent<Rigidbody>().isKinematic = false;
            L[i].GetComponent<BlockParent>().PairedPos.GetComponent<BoxCollider>().enabled = false;
            //L[i].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Nextsort;
            L[i].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().enabled = true;
            L[i].GetComponent<BlockParent>().gravity = true;

            Larray = true;
            waitingforswap = true;

            float.TryParse(b[k].GetComponentInChildren<Text>().text, out temp);
            float.TryParse(L[i].GetComponentInChildren<Text>().text, out temp2);
            if (temp == temp2)
            {
                waitingforswap = false;
            }
            while (waitingforswap)
            {
                yield return null;
            }
            b[k].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Donesort;
            L[i].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Donesort;

            bclone[k].SetActive(true);
            bclone[k].GetComponentInChildren<Text>().text = L[i].GetComponentInChildren<Text>().text;

            i++;
            k++;
            EnableTrigger(l, r);
            updatePos();
        }
        while (j < n2)
        {
            nextToSort = k;
            currentSmallestIndex = j;

            b[k].GetComponent<BlockParent>().enabled = true;
            b[k].GetComponent<BoxCollider>().enabled = true;
            b[k].GetComponent<Rigidbody>().isKinematic = false;
            pos[k].GetComponent<BoxCollider>().enabled = false;
            //pos[k].GetComponentInChildren<MeshRenderer>().material = Nextsort;
            pos[k].GetComponentInChildren<MeshRenderer>().enabled = true;
            b[k].GetComponent<BlockParent>().gravity = true;

            R[j].GetComponent<BlockParent>().enabled = true;
            R[j].GetComponent<BoxCollider>().enabled = true;
            R[j].GetComponent<Rigidbody>().isKinematic = false;
            R[j].GetComponent<BlockParent>().PairedPos.GetComponent<BoxCollider>().enabled = false;
            R[j].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Nextsort;
            //R[j].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().enabled = true;
            R[j].GetComponent<BlockParent>().gravity = true;

            Larray = false;
            waitingforswap = true;

            float.TryParse(b[k].GetComponentInChildren<Text>().text, out temp);
            float.TryParse(R[j].GetComponentInChildren<Text>().text, out temp2);
            if (temp == temp2)
            {
                waitingforswap = false;
            }
            while (waitingforswap)
            {
                yield return null;
            }
            b[k].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Donesort;
            R[j].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Donesort;

            bclone[k].SetActive(true);
            bclone[k].GetComponentInChildren<Text>().text = R[j].GetComponentInChildren<Text>().text;

            j++;
            k++;
            EnableTrigger(l, r);
            updatePos();
        }
        yield return new WaitForSeconds(speed);

        for (int x = 0; x < bclone.Length; x++)
        {
            b[x].GetComponentInChildren<Text>().text = bclone[x].GetComponentInChildren<Text>().text;
            bclone[x].GetComponentInChildren<Text>().text = b[x].GetComponentInChildren<Text>().text;
            bclone[x].SetActive(false);
        }
        updatePos();
    }

    public void EnableTrigger(int l, int h)
    {
        //Debug.Log(l + " " + h);
        for (int i = 0; i < b.Length; i++)
        {
            if (i >= l && i <= h)
            {
                b[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponent<Rigidbody>().isKinematic = true;
                b[i].GetComponent<BlockParent>().enabled = false;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                pos[i].GetComponentInChildren<MeshRenderer>().enabled = false;
                pos[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
                b[i].GetComponent<BlockParent>().gravity = false;
            }
            else
            {
                b[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponent<Rigidbody>().isKinematic = true;
                b[i].GetComponent<BlockParent>().enabled = false;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                pos[i].GetComponentInChildren<MeshRenderer>().enabled = false;
                pos[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                b[i].GetComponent<BlockParent>().gravity = false;
            }
        }
    }
}

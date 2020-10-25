using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOptimizationController : MonoBehaviour
{
    public GameObject[] objectsToHalt;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        for (int i = 0; i < objectsToHalt.Length; i++)
        {
            objectsToHalt[i].SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Leave");
        for (int i = 0; i < objectsToHalt.Length; i++)
        {
            objectsToHalt[i].SetActive(false);
        }
    }
}

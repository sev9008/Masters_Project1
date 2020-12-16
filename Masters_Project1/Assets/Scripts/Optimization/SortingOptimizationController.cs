using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script will enable and disable every object in: objectstohalt
/// WHen the player leaves the specified area the objects will be disabled.
/// When the player enters the specified area the objects will be enabled.
/// This script will also stop any coroutines and scripts currently being performed by any deactivated game object
/// </summary>
public class SortingOptimizationController : MonoBehaviour
{
    public GameObject[] objectsToHalt;
    private void Start()
    {
        for (int i = 0; i < objectsToHalt.Length; i++)
        {
            objectsToHalt[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < objectsToHalt.Length; i++)
            {
                objectsToHalt[i].SetActive(true);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < objectsToHalt.Length; i++)
            {
                objectsToHalt[i].SetActive(false);
            }
        }

    }
}

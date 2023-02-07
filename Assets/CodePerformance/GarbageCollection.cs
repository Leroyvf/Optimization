using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollection : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //GarbageCreation();
        NoGarbageCreation();
    }

    private void GarbageCreation()
    {
        GameObject[] children = new GameObject[5];
        for (int i = 0; i < 5; i++)
            children[i] = transform.GetChild(i).gameObject;
    }

    GameObject[] childrenNoGarbage = new GameObject[5];
    private void NoGarbageCreation()
    {
        for (int i = 0; i < 5; i++)
            childrenNoGarbage[i] = transform.GetChild(i).gameObject;
    }
}

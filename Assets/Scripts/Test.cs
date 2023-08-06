using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject test;
    public GameObject glasses;

    public void TestQ()
    {
        test.SetActive(!test.activeSelf);
        glasses.SetActive(test.activeSelf);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) TestQ();
    }
}

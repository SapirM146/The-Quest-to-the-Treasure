using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor.UIElements;
using UnityEngine;

public class BillBoardScript : MonoBehaviour
{
    private Transform cam;
    //private Transform player;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //cam = Camera.main.transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        transform.LookAt(transform.position + cam.forward);
    }
}

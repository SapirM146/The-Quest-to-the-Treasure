using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardScript : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        transform.LookAt(transform.position + cam.forward);
    }
}

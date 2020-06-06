using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardScript : MonoBehaviour
{
    //public Transform cam;
    private Transform player;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(transform.position + player.forward);
    }
}

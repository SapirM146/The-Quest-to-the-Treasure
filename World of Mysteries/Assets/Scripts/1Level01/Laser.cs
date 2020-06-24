using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer laser;
    public CapsuleCollider2D collider2d;
    public Transform endPoint;

    // Update is called once per frame
    void Update()
    {
        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, endPoint.position);

        // add if statment that will power down the laser if complete task
        //if ()
        //{
        //    laser.enabled = false;
        //    collider2d.enabled = false;
        //}
    }
}

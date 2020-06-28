using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class MissileLauncherScript : MonoBehaviour
{
    public Transform partToRotate;
    public float turnSpeed = 1f;
    public float currntTurnAngle = 90f;

    // Update is called once per frame
    void Update()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        
        if (h != 0)
        {
            //currntTurnAngle = currntTurnAngle - turnSpeed * Time.deltaTime;
            currntTurnAngle = partToRotate.transform.rotation.y + h * turnSpeed * Time.deltaTime;
            partToRotate.transform.rotation = Quaternion.Euler(0f, currntTurnAngle, 0f);
        }
    }
}

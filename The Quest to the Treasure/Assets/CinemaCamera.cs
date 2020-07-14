using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaCamera : MonoBehaviour
{
    //float endPosForword = 160f; // z
    float endPosForword = 115f; // z

    float endPosUp = 75f; // y

    float endPosLeft = 0f; // x
    float startPosRight = 0f; // x

    bool forwardMotion;
    bool upMotion;

    // Start is called before the first frame update
    void Start()
    {
        forwardMotion = true;
        transform.position = new Vector3(120, 30, 12);
        transform.eulerAngles = new Vector3(54f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (forwardMotion)
            moveForward();
        else if (upMotion)
            moveUpBackward();

    }

    void moveForward()
    {
        if (transform.position.z < endPosForword)
            transform.position += new Vector3(0, 0, 10f * Time.deltaTime);
        else
        {
            forwardMotion = false;
            upMotion = true;
            transform.eulerAngles = new Vector3(70f, 0f, 0f);
        }
    }

    void moveUpBackward()
    {
        if (transform.position.y < endPosUp)
        {
            transform.position += new Vector3(0, 5f * Time.deltaTime, -2f * Time.deltaTime);
            Debug.Log(transform.position);
        }
        else
            upMotion = false;
    }
}

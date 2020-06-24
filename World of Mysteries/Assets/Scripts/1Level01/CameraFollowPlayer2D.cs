using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer2D : MonoBehaviour
{

    public Transform player;
    public Transform mapPos;

    float x_offset = 7.8f;
    float y_offset = 4.4f;
    bool levelComplete = false;
    float x_CameraPosToPlayerRight;
    float x_CameraPosToPlayerLeft; 
    float y_CameraPosToPlayerUp;
    float y_CameraPosToPlayerDown;

    private void Start()
    {
        //cam = GetComponent<Camera>();
        //goToMap();
        recalculateCameraPos();

    }

    private void FixedUpdate()
    {
        if (levelComplete)
        {
            if((transform.position.y - mapPos.position.y) < 1.5f)
            {
                transform.position += new Vector3(0, 0.1f * Time.fixedDeltaTime, 0);
            }
            else
            {
                // go to next level
            }
        }
    }

    void LateUpdate()
    {
        if (player.position.x > x_CameraPosToPlayerRight)
        {
            move(x_offset, 0);
            recalculateCameraPos();
        }
        else if (player.position.x < x_CameraPosToPlayerLeft)
        {
            move(-x_offset, 0);
            recalculateCameraPos();
        }
        else if (player.position.y > y_CameraPosToPlayerUp)
        {
            move(0, y_offset);
            recalculateCameraPos();
        }

        else if (player.position.y < y_CameraPosToPlayerDown)
        {
            move(0, -y_offset);
            recalculateCameraPos();
        }



        //Vector3 newPositon = player.position;
        //newPositon.z = transform.position.z;
        //transform.position = newPositon;

        //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f); // rotate with player
    }

    void recalculateCameraPos()
    {
        x_CameraPosToPlayerRight = transform.position.x + x_offset / 2;
        x_CameraPosToPlayerLeft = transform.position.x - x_offset / 2;
        y_CameraPosToPlayerUp = transform.position.y + y_offset / 2;
        y_CameraPosToPlayerDown = transform.position.y - y_offset / 2;
    }

    public void move(float x, float y)
    {
        transform.position += new Vector3(x, y, 0);
    }

    public void goToMap()
    {
        Vector3 newPositon = mapPos.position;
        newPositon.z = transform.position.z;
        transform.position = newPositon;
        levelComplete = true;
    }
}

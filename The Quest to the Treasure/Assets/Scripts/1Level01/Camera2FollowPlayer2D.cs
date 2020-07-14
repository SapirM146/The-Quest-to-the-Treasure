using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2FollowPlayer2D : MonoBehaviour
{
    public Transform player;
    public Transform mapPos;
    public L1GameManager gameManager;

    float x_offset = 7.8f;
    float y_offset = 4.4f;
    float x_CameraPosToPlayerRight;
    float x_CameraPosToPlayerLeft; 
    float y_CameraPosToPlayerUp;
    float y_CameraPosToPlayerDown;

    bool moveCameraToEndOfLevel;
    bool waitingToNextLevel;


    private void Start()
    {
        moveCameraToEndOfLevel = false;
        waitingToNextLevel = false;
        recalculateCameraPos();
    }

    private void FixedUpdate()
    {
        if (moveCameraToEndOfLevel)
        {
            if((transform.position.y - mapPos.position.y) < 1.5f)
            {
                transform.position += new Vector3(0, 0.1f * Time.fixedDeltaTime, 0);
            }
            else if(!waitingToNextLevel)
            {
                waitingToNextLevel = true;
                StartCoroutine(waitToNextLevel()); // go to next level
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
        moveCameraToEndOfLevel = true;
    }

    IEnumerator waitToNextLevel()
    {
        yield return new WaitForSeconds(0.5f);
        gameManager.GoToNextLevel();
    }
}

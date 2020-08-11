using System.Collections;
using UnityEngine;


public class CameraFollowPlayer2D : MonoBehaviour
{
    public Transform player;
    public Transform mapPos;
    PlayerMovement playerMovement;
    PlayerCollect playerCollect;

    readonly float x_offset = 7.8f;
    readonly float y_offset = 4.4f;
    float x_CameraPosToPlayerRight;
    float x_CameraPosToPlayerLeft; 
    float y_CameraPosToPlayerUp;
    float y_CameraPosToPlayerDown;
    Vector3 oldPosition;

    public bool MoveCameraToMap { get; private set; }


    private void Start()
    {
        MoveCameraToMap = false;
        playerMovement = player.GetComponent<PlayerMovement>();
        playerCollect = player.GetComponent<PlayerCollect>();
        recalculateCameraPos();
    }

    private void FixedUpdate()
    {
        if (MoveCameraToMap)
        {
            if ((transform.position.y - mapPos.position.y) < 1.5f)
            {
                transform.position += new Vector3(0, 0.1f * Time.fixedDeltaTime, 0);
            }
            else
            {
                MoveCameraToMap = false;
                StartCoroutine(returnToPlayerPosition());// move to IEnumerator function
            }
        }
    }

    void LateUpdate()
    {
        if (!MoveCameraToMap)
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
        StartCoroutine(goToMapPosition());
    }

    IEnumerator goToMapPosition()
    {
        yield return new WaitForSeconds(0.5f);
        oldPosition = transform.position;
        playerMovement.isStopped = true;
        Vector3 newPositon = mapPos.position;
        newPositon.z = transform.position.z;
        transform.position = newPositon;
        MoveCameraToMap = true;
        playerCollect.showCollectPanels(false);
    }

    IEnumerator returnToPlayerPosition()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = oldPosition;
        playerCollect.showCollectPanels(true);
        playerMovement.isStopped = false;
    }
}

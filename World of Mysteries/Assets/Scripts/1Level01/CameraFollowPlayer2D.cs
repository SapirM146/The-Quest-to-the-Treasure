using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer2D : MonoBehaviour
{

    public Transform player;

    void LateUpdate()
    {
        Vector3 newPositon = player.position;
        newPositon.z = transform.position.z;
        transform.position = newPositon;

        //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f); // rotate with player
    }
}

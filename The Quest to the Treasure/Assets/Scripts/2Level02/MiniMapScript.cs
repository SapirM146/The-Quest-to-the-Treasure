using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPositon = player.position;
        newPositon.y = transform.position.y;
        transform.position = newPositon;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f); // rotate with player
    }
}

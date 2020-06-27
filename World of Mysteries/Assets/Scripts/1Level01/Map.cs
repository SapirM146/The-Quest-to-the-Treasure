using UnityEngine;

public class Map : MonoBehaviour
{
    public PlayerCollect playerCollect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCollect.collectMap();
            Destroy(gameObject);
        }
    }
}

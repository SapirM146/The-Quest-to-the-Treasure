using UnityEngine;

public class Key : MonoBehaviour
{
    public PlayerCollect playerCollect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCollect.collectKey();
            Destroy(gameObject);
        }
    }
}

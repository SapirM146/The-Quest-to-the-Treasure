using UnityEngine;

public class Gem : MonoBehaviour
{
    PlayerCollect playerCollect;

    private void Start()
    {
        playerCollect = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollect>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCollect.collectGem();
            Destroy(gameObject);
        }
    }
}

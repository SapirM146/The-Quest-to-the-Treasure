using UnityEngine;

public class L1TriggerFinishGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            L1GameManager.playerInEndZone = true;
        }
    }
}

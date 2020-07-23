using UnityEngine;

public class TriggerFinishGameScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            L2GameManagerScript.playerInEndZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            L2GameManagerScript.playerInEndZone = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinishGameScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            L2GameManagerScript.playerInEndZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            L2GameManagerScript.playerInEndZone = false;
        }
    }
}

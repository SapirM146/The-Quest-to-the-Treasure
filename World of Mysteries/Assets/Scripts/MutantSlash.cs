using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantSlash : MonoBehaviour
{
    public PlayerHPScript playerHP;
    public MutantMovementScript movment;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && movment.playerAttacked)
        {
            Debug.Log("hit player");
            playerHP.takeDamage(10);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashHitTrigger : MonoBehaviour
{
    public PlayerHPScript playerHP;
    public int damage = 30;
    //public MutantMovementScript movment;


    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player") && movment.playerAttacked)
        if (other.CompareTag("Player"))
        {
            Debug.Log("hit player");
            playerHP.takeDamage(damage);
        }
    }

}

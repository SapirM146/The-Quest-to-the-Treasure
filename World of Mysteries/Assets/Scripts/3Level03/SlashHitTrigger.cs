using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashHitTrigger : MonoBehaviour
{
    public PlayerHPScript playerHP;
    public int damage = 30;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHP.takeDamage(damage);
        }
    }

}

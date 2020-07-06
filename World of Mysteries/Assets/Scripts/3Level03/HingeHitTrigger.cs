using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeHitTrigger : MonoBehaviour
{
    public HingeObstacle obstacle;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if(other.CompareTag("Player"))
            obstacle.hitPlayer();
    }
}

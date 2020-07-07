using UnityEngine;

public class HingeHitTrigger : MonoBehaviour
{
    public HingeObstacle obstacle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            obstacle.hitPlayer();
    }
}

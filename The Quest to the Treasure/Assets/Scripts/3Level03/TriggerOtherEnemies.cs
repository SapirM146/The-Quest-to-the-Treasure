using UnityEngine;

public class TriggerOtherEnemies : MonoBehaviour
{
    public GameObject backupEnemies;

    private void OnTriggerEnter(Collider other)
    {
        backupEnemies.SetActive(true);
    }
}

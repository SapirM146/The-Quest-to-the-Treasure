using UnityEngine;

public class RockBreak : MonoBehaviour
{
    public GameObject breakingRock;

    public void RockExplode()
    {
        Instantiate(breakingRock, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

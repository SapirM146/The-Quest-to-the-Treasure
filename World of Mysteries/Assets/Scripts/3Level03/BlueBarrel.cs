using UnityEngine;

public class BlueBarrel : MonoBehaviour
{
    public GameObject destroyedBarrel;

    public void explode()
    {
        Instantiate(destroyedBarrel, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

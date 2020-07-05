using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion; // put the destroy sound on this object (active on awake)
    bool hasDestroyed;

    private void Start()
    {
        hasDestroyed = false;
    }

    public void Destroy()
    {
        if (!hasDestroyed)
        {
            hasDestroyed = true;
            if (destroyedVersion != null)
                Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
       
    }
}

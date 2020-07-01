using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    public GameObject destroyedBarrel;
    public Transform explosionPos;
    public AudioClip explosionSound;

    public void explode()
    {
        AudioSource.PlayClipAtPoint(explosionSound,transform.position);
        ParticleSystem explosion = Instantiate(explosionEffect, explosionPos.position, Quaternion.identity);
        Instantiate(destroyedBarrel, transform.position, Quaternion.identity);
        Destroy(explosion.gameObject, 2f);
        Destroy(gameObject);
    }
}

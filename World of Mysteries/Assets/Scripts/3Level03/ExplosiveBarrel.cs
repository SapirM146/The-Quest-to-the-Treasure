using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    public GameObject destroyedBarrel;
    public Transform explosionPos;
    AudioSource explosionSound;
    MeshRenderer mesh;
    CapsuleCollider capsuleCollider;

    private void Start()
    {
        explosionSound = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void explode()
    {
        explosionSound.Play();
        ParticleSystem explosion = Instantiate(explosionEffect, explosionPos.position, Quaternion.identity);
        mesh.enabled = false;
        capsuleCollider.enabled = false;
        Instantiate(destroyedBarrel, transform.position, Quaternion.identity);
        Destroy(explosion.gameObject, 2f);
        Destroy(gameObject, 3f);
    }
}

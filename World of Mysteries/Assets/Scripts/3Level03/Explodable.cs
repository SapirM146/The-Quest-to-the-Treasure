using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour
{
    public GameObject destroyedVersion;
    public ParticleSystem explosionEffect;
    public float radius = 5f;
    public float force = 700f;
    public int damage;
    bool hasExploded;
    L3GameManager gm;


    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<L3GameManager>();
        hasExploded = false;
    }

    public void Explode()
    {
        if (!hasExploded)
        {
            hasExploded = true;
            if (destroyedVersion != null)
                Instantiate(destroyedVersion, transform.position, transform.rotation);

            ParticleSystem explosion = Instantiate(explosionEffect, transform.position, transform.rotation);

            Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in collidersToDestroy)
            {
                Destructible dest = nearbyObject.transform.GetComponent<Destructible>();
                if (dest != null)
                {
                    dest.Destroy();
                    continue;
                }

                Explodable explodable = nearbyObject.transform.GetComponent<Explodable>();
                if (explodable != null)
                {
                    explodable.Explode();
                    continue;
                }

                EnemyHPScript target = nearbyObject.GetComponent<EnemyHPScript>();
                if (target != null)
                {
                    gm.showDamageOnScreen(damage);
                    target.takeDamage(damage);
                    continue;
                }
            }

            Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in collidersToMove)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(force, transform.position, radius);
            }

            Destroy(explosion.gameObject, 2f);
            Destroy(gameObject);
        }
    }
}

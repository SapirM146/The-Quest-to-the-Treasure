using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class ExplosionPhysicsForce : MonoBehaviour
    {
        public float explosionForce = 4;
        public float range = 5;
        public LayerMask layerMask;
        L3GameManager gm;


        private IEnumerator Start()
        {
            //// wait one frame because some explosions instantiate debris which should then
            //// be pushed by physics force
            yield return null;

            float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<L3GameManager>();

            range *= multiplier;
            var cols = Physics.OverlapSphere(transform.position, range, layerMask);

            var rigidbodies = new List<Rigidbody>();
            foreach (var col in cols)
            {
                if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
                {
                    rigidbodies.Add(col.attachedRigidbody);
                }
            }
            foreach (var rb in rigidbodies)
            {
                rb.AddExplosionForce(explosionForce * multiplier, transform.position, range, 1 * multiplier, ForceMode.Impulse);
                EnemyHPScript enemy = rb.gameObject.GetComponent<EnemyHPScript>();
                if (enemy != null)
                {
                    int amount = 40;
                    gm.showDamageOnScreen(amount);
                    enemy.takeDamage(40);

                }
            }
        }
    }
}

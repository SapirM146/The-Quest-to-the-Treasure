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

        private IEnumerator Start()
        {
            //// wait one frame because some explosions instantiate debris which should then
            //// be pushed by physics force
            yield return null;

            float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;

            //float r = 1 * multiplier;
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
                    enemy.takeDamage(40);
            }
        }

        //private void FixedUpdate()
        //{
        //    float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;

        //    //float r = 1 * multiplier;
        //    range *= multiplier;
        //    var cols = Physics.OverlapSphere(transform.position, range, layerMask);

        //    var rigidbodies = new List<Rigidbody>();
        //    foreach (var col in cols)
        //    {
        //        if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
        //        {
        //            rigidbodies.Add(col.attachedRigidbody);
        //        }
        //    }
        //    foreach (var rb in rigidbodies)
        //    {
        //        rb.AddExplosionForce(explosionForce * multiplier, transform.position, range, 1 * multiplier, ForceMode.Impulse);
        //        EnemyHPScript enemy = rb.gameObject.GetComponent<EnemyHPScript>();
        //        if (enemy != null)
        //            enemy.takeDamage(40);
        //    }
        //}

        //private void OnDrawGizmos()
        //{
        //    Gizmos.DrawWireSphere(transform.position, range);
        //}
    }
}

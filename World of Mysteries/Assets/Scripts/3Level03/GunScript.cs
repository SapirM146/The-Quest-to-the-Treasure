using UnityEngine;

public class GunScript : MonoBehaviour
{

    public int damage = 10;
    public float range = 15f;
    public LayerMask layerMask;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    AudioSource laserShotSound;
    public L3GameManager gm;


    private void Start()
    {
        laserShotSound = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
    }

    private void shoot()
    {
        laserShotSound.Play();
        muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask))
        {
            Destructible dest = hit.transform.GetComponent<Destructible>();
            if (dest != null)
                dest.Destroy();
            
            Explodable explodable = hit.transform.GetComponent<Explodable>();
            if (explodable != null)
                explodable.Explode();

            //else if (hit.collider.CompareTag("Enemy"))
            //{
            //    EnemyHPScript target = hit.transform.GetComponent<EnemyHPScript>();
            //    if (target != null)
            //    {
            //        gm.showDamageOnScreen(damage);
            //        target.takeDamage(damage);
            //    }
            //}
            //else if (hit.collider.CompareTag("EnemyHead"))
            //{
            //    EnemyHPScript target = hit.transform.GetComponent<EnemyHPScript>();

            //    if (target != null)
            //    {
            //        int amount = damage * 2;
            //        gm.showDamageOnScreen(amount);
            //        target.takeDamage(amount);
            //    }
            //}

            EnemyHPScript target = hit.transform.GetComponent<EnemyHPScript>();
            if (target != null)
            {
                int amount = damage;

                if (hit.collider.CompareTag("EnemyHead"))
                        amount *= 2;

                gm.showDamageOnScreen(amount);
                target.takeDamage(amount);
            }

            GameObject impactGO2 = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO2, 1f);
        }
    }
}

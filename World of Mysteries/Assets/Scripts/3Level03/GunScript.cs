using UnityEngine;

public class GunScript : MonoBehaviour
{

    public int damage = 10;
    public float range = 15f;
    public LayerMask layerMask;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public L3GameManager gm;

    
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
    }

    private void shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHPScript target = hit.transform.GetComponent<EnemyHPScript>();
                if (target != null)
                {
                    gm.showDamageOnScreen(damage);
                    target.takeDamage(damage);
                }
            }
            if (hit.collider.CompareTag("EnemyHead"))
            {
                EnemyHPScript target = hit.transform.GetComponent<EnemyHPScript>();

                if (target != null)
                {
                    int amount = damage * 2;
                    gm.showDamageOnScreen(amount);
                    target.takeDamage(amount);
                }
            }
            else if (hit.collider.CompareTag("Barrel"))
            {
                ExplosiveBarrel barrel = hit.transform.GetComponent<ExplosiveBarrel>();
                if (barrel != null)
                {
                    barrel.explode();
                }
                //else
                //{
                //    BlueBarrel blue = hit.transform.GetComponent<BlueBarrel>();
                //    blue.explode();
                //}

            }

            GameObject impactGO2 = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO2, 1f);
        }
    }
}

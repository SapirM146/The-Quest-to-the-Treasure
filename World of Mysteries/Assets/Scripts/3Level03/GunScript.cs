using UnityEngine;

public class GunScript : MonoBehaviour
{

    public int damage = 10;
    public float range = 30f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    

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
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            EnemyHPScript target = hit.transform.GetComponent<EnemyHPScript>();
            if(target != null)
            {
                target.takeDamage(damage);
            }
            GameObject impactGO2 = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO2, 2f);
        }
    }
}

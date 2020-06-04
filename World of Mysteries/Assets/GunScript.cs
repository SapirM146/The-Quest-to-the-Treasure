using System;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public int damage = 10;
    public float range = 100f;
    public Camera fpsCam;
    //public ParticleSystem muzzleFlash;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
    }

    private void shoot()
    {
        //muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            EnemyHPScript target = hit.transform.GetComponent<EnemyHPScript>();
            if(target != null)
            {
                target.takeDamage(damage);
            }
        }
    }
}

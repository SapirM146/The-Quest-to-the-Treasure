using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb;
    PlayerHPScript playerHP;
    Vector3 startPos;
    int force = 10000;
    int maxDistance = 300;

    public GameObject origin;
    AudioSource cannonSound;

    private void Awake()
    {
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHPScript>();
        cannonSound = playerHP.gameObject.GetComponents<AudioSource>()[2];
    }

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(force * origin.transform.forward);
    }

    void Update()
    {
            float dis = Vector3.Distance(transform.position, startPos);
            if (dis > maxDistance)
                Destroy(gameObject);   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (origin.CompareTag("Player"))
        {
            if (other.CompareTag("EnemyBody"))
            {
                cannonSound.Play();
                //other.GetComponent<EnemyHPScript>().takeDamage(20);
                other.GetComponent<EnemyBoatPartScript>().MainPartHP.takeDamage(20);
                //Destroy(other.gameObject);
            }
        }
        
        else // origin is enemy
        {
            //if (other.CompareTag("PlayerBody"))
            if (other.CompareTag("Player"))
            {
                cannonSound.Play();
                playerHP.takeDamage(15);
            }
        }

        Destroy(gameObject);
    }
}

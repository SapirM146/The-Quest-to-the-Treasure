using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb;
    //PlayerHPScript playerHP;
    Vector3 startPos;
    int force = 10000;
    int maxDistance = 300;

    public GameObject origin;
    AudioSource cannonSound;

    private void Awake()
    {
        cannonSound = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>()[2];
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
            EnemyHPScript enemy = other.GetComponent<EnemyHPScript>();
            if (enemy != null)
            {
                cannonSound.Play();
                enemy.takeDamage(20);
            }
        }
        
        else // origin is enemy
        {
            PlayerHPScript player = other.GetComponent<PlayerHPScript>();
            if (player != null)
            {
                cannonSound.Play();
                player.takeDamage(15);
            }
        }

        Destroy(gameObject);
    }
}

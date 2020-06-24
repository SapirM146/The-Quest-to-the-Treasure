using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoatRamed : MonoBehaviour
{

    EnemyHPScript hp;
    AudioSource wood_hit_sound;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<EnemyHPScript>();
        wood_hit_sound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.CompareTag("Player"))
        {
            wood_hit_sound.Play();
            hp.takeDamage(30);
        }

    }
}

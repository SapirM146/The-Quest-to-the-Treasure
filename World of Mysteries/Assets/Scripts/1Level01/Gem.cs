using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    GameObject player;
    //PlayerCollect playerCollect;

    // Start is called before the first frame update
    void Start()
    {
        //playerCollect = player.GetComponent<PlayerCollect>()
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // playerCollect.collectGem(); // in function --> gem collect sound

            Destroy(gameObject);
        }
    }
}

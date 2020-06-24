using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
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
            // playerCollect.collectCoin(); // in function --> coin collect sound

            Destroy(gameObject);
        }
    }
}

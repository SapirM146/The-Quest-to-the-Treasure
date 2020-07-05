using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float delay = 3f;
    float countdown;
    bool hasExploded;
    Explodable explodable;
    L3GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<L3GameManager>();
        explodable = GetComponent<Explodable>();
        hasExploded = false;
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        groundCheck(gm.CurrentStage + 1);
        if (countdown <= 0 && !hasExploded)
        {
            explodable.Explode();
            hasExploded = true; 
        }
    }

    void groundCheck(int stage)
    {
       
        float minHeight = 5f * stage;
        if (transform.position.y <= minHeight)
        {
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
        }
    }
}

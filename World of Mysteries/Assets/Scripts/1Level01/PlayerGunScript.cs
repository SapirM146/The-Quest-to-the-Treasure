using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;

public class PlayerGunScript : MonoBehaviour
{

    //public RigidbodyFirstPersonController fps;
    public GameObject fpsCharacter;
    public GameObject tpsCharacter;

    bool inAimMode = false;
    Vector3 offset;


    private void Start()
    {
        offset = new Vector3(0, 1.3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            inAimMode = !inAimMode;
            aimMode();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(inAimMode)
                shoot();
        }
    }

    private void shoot()
    {
        // Make shoot animation

        // Decrease enemy HP 
        // enemy.hitDamage(0.3f);
    }

    void checkGround(GameObject player)
    {
        if (player.transform.position.y < 0)
        {
            Vector3 old = player.transform.position;
            player.transform.position = new Vector3(old.x, 0, old.z);
        } 
    }

    void aimMode()
    {
        if (inAimMode)
        {
            fpsCharacter.transform.position = tpsCharacter.transform.position + new Vector3(0, 0.15f, 0);
            fpsCharacter.transform.rotation = tpsCharacter.transform.rotation;
            checkGround(fpsCharacter);
            checkGround(tpsCharacter);
            tpsCharacter.SetActive(false);
            fpsCharacter.SetActive(true);
        }
        else
        {
            tpsCharacter.transform.position = fpsCharacter.transform.position - offset;
            tpsCharacter.transform.rotation = fpsCharacter.transform.rotation;
            checkGround(fpsCharacter);
            checkGround(tpsCharacter);
            fpsCharacter.SetActive(false);
            tpsCharacter.SetActive(true);
        }
    }
}

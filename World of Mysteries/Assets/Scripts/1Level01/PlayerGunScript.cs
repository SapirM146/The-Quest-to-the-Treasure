﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimModeScript : MonoBehaviour
{

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
            checkGround(fpsCharacter);
            checkGround(tpsCharacter);
            fpsCharacter.transform.position = tpsCharacter.transform.position + new Vector3(0, 0.15f, 0);
            fpsCharacter.transform.rotation = tpsCharacter.transform.rotation;
            tpsCharacter.SetActive(false);
            fpsCharacter.SetActive(true);
        }
        else
        {
            checkGround(fpsCharacter);
            checkGround(tpsCharacter);
            tpsCharacter.transform.position = fpsCharacter.transform.position - offset;
            tpsCharacter.transform.rotation = fpsCharacter.transform.rotation;
            fpsCharacter.SetActive(false);
            tpsCharacter.SetActive(true);
        }
    }
}

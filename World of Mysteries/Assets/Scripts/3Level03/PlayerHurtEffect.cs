﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtEffect : MonoBehaviour
{
    public GameObject hurtCanvas;
    PlayerHPScript hp;
    Animator hurtAnimator;
    int currentHP;
    AudioSource hurtSound;
    AudioSource hurtBadSound;

    private void Awake()
    {
        hurtAnimator = hurtCanvas.GetComponent<Animator>();
        AudioSource[] audios = GetComponents<AudioSource>();
        hurtSound = audios[0];
        hurtBadSound = audios[1];
        hp = GetComponent<PlayerHPScript>();
        currentHP = hp.CurrentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        int hpCheck = hp.CurrentHealth;

        if (hpCheck <= currentHP * 0.4)
        {
            hurtAnimator.SetTrigger("PlayerGetHit");
            hurtBadSound.Play();
        }

        else if (hpCheck < currentHP)
        {
            hurtAnimator.SetTrigger("PlayerGetHit");
            hurtSound.Play();
        }
        
        currentHP = hpCheck;
    }


}

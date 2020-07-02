using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtEffect : MonoBehaviour
{
    public GameObject hurtCanvas;
    PlayerHPScript hp;
    Animator hurtAnimator;
    int currentHP;
    AudioSource hurtSound;
    public AudioClip hurtByBoss;

    private void Awake()
    {
        hurtAnimator = hurtCanvas.GetComponent<Animator>();
        hurtSound = GetComponent<AudioSource>();
        hp = GetComponent<PlayerHPScript>();
        currentHP = hp.CurrentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        int hpCheck = hp.CurrentHealth;

        if (hpCheck < currentHP)
        {
            hurtAnimator.SetTrigger("PlayerGetHit");
            hurtSound.Play();
        }
        
        currentHP = hp.CurrentHealth;
    }


}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeThrow : MonoBehaviour
{
    public GameObject grenadePrefab;
    public float throwForce = 20f;
    public int amountToThrow = 3;
    public float reloadTime = 60f;
    public Text grenadeAmountText;


    float countdown;
    int currentAmountToThrow;

    private void Start()
    {
        currentAmountToThrow = amountToThrow;
        countdown = reloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmountToThrow == 0)
        {
            if (countdown > 0)
                countdown -= Time.deltaTime;
            else
            {
                currentAmountToThrow = amountToThrow;
                countdown = reloadTime;
            }
        }

        if (currentAmountToThrow > 0 && Input.GetMouseButtonDown(1))
        {
            --currentAmountToThrow;
            ThrowGrenade();
        }

        grenadeAmountText.text = currentAmountToThrow.ToString();
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        rb.AddTorque(new Vector3(30, 20, 10));
    }
}

﻿using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class FoundPlayerTrigger : MonoBehaviour
{
    public AICharacterControlWithPatrol ai;
    public bool foundPlayer;
    [HideInInspector]
    public Transform player;


    private void Start()
    {
        foundPlayer = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foundPlayer = true;
            player = other.gameObject.transform;
            ai.SetTarget(player);
            ai.agent.speed = ai.runSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foundPlayer = false;
            ai.SetTarget(null);
            ai.agent.speed = ai.walkSpeed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    Animator animator;
    bool playerFound = false;
    bool isAlive = true;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerFound = true;
        animator.Play("Eats");
    }

    void Update()
    {
        if (isAlive)
        {
            if (playerFound)
            {
                ChasePlayer();
            }
        }
        
    }

    void foundPlayer()
    {
        animator.Play("shout");
        playerFound = true;
    }

    void ChasePlayer()
    {
        float dis = Vector3.Distance(transform.position, player.position);

        if (dis <= agent.stoppingDistance)
        {
            transform.LookAt(player);
            animator.Play("Attack");
        }
        //else if (dis < agent.stoppingDistance + 5)
        //{
        //    agent.speed = 1.5f;
        //    animator.SetTrigger("Walk");
        //}
        else
        {
            agent.speed = 3.5f;
            animator.SetTrigger("Run");
        }

        agent.SetDestination(player.position);
    }

    void getHit()
    {
        // reduce HP
        animator.Play("Get_Hit");
    }


    void Dead()
    {
        isAlive = false;
        animator.Play("Dead");
        Destroy(gameObject, 5f);
    }

}

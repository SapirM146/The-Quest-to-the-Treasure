using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MutantMovementScript : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    Animator animator;
    Coroutine attack;
    bool playerFound;
    bool playerChase;
    public bool playerAttacked;
    public float range = 30f;

    public EnemyHPScript hp_script;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerFound = false;
        playerChase = false;
        playerAttacked = false;
        //StartCoroutine(foundPlayer());
        //animator.Play("Eats");
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (hp_script.isAlive)
        {
            RaycastHit hit;

            if (playerChase)
            {
                ChasePlayer();
            }

            else if (!playerFound && Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("found");
                    StartCoroutine(foundPlayer());
                    playerFound = true;
                }
            }
        }
        else
        {
            agent.isStopped = true;
            animator.StopPlayback();
        }

    }

    IEnumerator foundPlayer()
    {
        transform.LookAt(player);
        animator.SetTrigger("Roaring");

        yield return new WaitForSeconds(1f);

        playerChase = true;
    }


    void ChasePlayer()
    {
        float dis = Vector3.Distance(transform.position, player.position);

        if (dis <= agent.stoppingDistance)
        {
            agent.speed = 0f;
            animator.SetBool("Running", false);
            if (!playerAttacked)
            {
                playerAttacked = true;
                attack = StartCoroutine(attackPlayer());
            }

        }
        else if (dis < agent.stoppingDistance + 5)
        {
            agent.speed = 1.5f;
            animator.SetBool("Walking", true);
        }
        else
        {
            if (playerAttacked)
            {
                playerAttacked = false;
                StopCoroutine(attack);
                //StopCoroutine(attackPlayer());
            }
            animator.SetBool("Running", true);
            agent.speed = 3.5f;

        }

        agent.SetDestination(player.position);
    }

    IEnumerator attackPlayer()
    {
        transform.LookAt(player);
        animator.Play("Swiping", -1, 0f);
        //animator.SetBool("Attacking", true);
        //playerHP.takeDamage(10);

        yield return new WaitForSeconds(2f);

        playerAttacked = false;
        //animator.SetBool("Attacking", false);


    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        playerHP.takeDamage(10);
    //    }
    //}

}

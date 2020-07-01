using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RhinoMovementScript : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    Animator animator;
    Coroutine attack;
    bool playerFound = false;
    bool playerChase = false;
    bool playerAttacked = false;
    public float range = 30f;
    LayerMask layerMask = 1 << 10;     // Bit shift the index of the layer (10) to get a bit mask

    public EnemyHPScript hp_script;
    public PlayerHPScript playerHP;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //StartCoroutine(foundPlayer());
        //animator.Play("Eats");
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (hp_script.isAlive)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward, Color.blue);

            if (playerChase)
            {
                ChasePlayer();
            }
            else if (!playerFound && Physics.Raycast(transform.position, transform.forward, out hit, range, layerMask))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("found");
                    StartCoroutine(foundPlayer());
                    playerFound = true;
                }
            }
        }
        //else
        //{
        //    agent.isStopped = true;
        //    animator.StopPlayback();
        //}

    }

    IEnumerator foundPlayer()
    {
        transform.LookAt(player);
        animator.Play("shout");

        yield return new WaitForSeconds(1f);

        playerChase = true;
    }


    void ChasePlayer()
    {
        float dis = Vector3.Distance(transform.position, player.position);
        Debug.Log(dis);

        if (dis <= agent.stoppingDistance)
        {
            //agent.velocity = Vector3.zero;
            //Debug.Log(agent.velocity.magnitude);
            
            
                //animator.SetBool("Run", false);
            if (!playerAttacked)
            {
                playerAttacked = true;
                attack = StartCoroutine(attackPlayer());
            }

        }
        //else if (dis < agent.stoppingDistance + 5)
        //{
        //    agent.speed = 1.5f;
        //    animator.SetTrigger("Walk");
        //}
        else
        {
            if (playerAttacked)
            {
                playerAttacked = false;
                StopCoroutine(attack);
                //StopCoroutine(attackPlayer());
            }
                //animator.SetBool("Run", true);
            //agent.speed = 3.5f;

        }

        agent.SetDestination(player.position);
    }

    IEnumerator attackPlayer()
    {
        transform.LookAt(player);
        animator.Play("Attack", -1, 0f);
        //playerHP.takeDamage(10);

        yield return new WaitForSeconds(2f);

        playerAttacked = false;

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        playerHP.takeDamage(10);
    //    }
    //}

}

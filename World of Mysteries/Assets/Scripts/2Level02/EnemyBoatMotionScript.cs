using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBoatMotionScript : MonoBehaviour
{
    public Transform[] waypoints;
    public GameObject explosion;
    Transform player;
    RaycastHit rayHit;
    Ray sight;
    NavMeshAgent agent;
    EnemyHPScript hp;

    int currentWayPoint;
    public bool isPlayerDetected = false;
    float maxDistance = 200f;
    float lostDistance;
    LayerMask layerMask = 1 << 10;     // Bit shift the index of the layer (10) to get a bit mask

    public Animator foundTextAnim;
    public Animator lostTextAnim;


    private void Start()
    {
        lostDistance = maxDistance + 20f;
        agent = GetComponent<NavMeshAgent>();
        hp = GetComponent<EnemyHPScript>();
        agent.autoBraking = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GotoNextPoint();
    }

    private void Update()
    {
        if (hp.isAlive)
        {
            // Choose the next destination point when the agent gets
            // close to the current one.
            if (!isPlayerDetected && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                GotoNextPoint();
        }
        else
        {
            agent.enabled = false;
            Instantiate(explosion, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
            this.enabled = false;
        }
    }

    void GotoNextPoint()
    {
        if (waypoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.SetDestination(waypoints[currentWayPoint].position);

        currentWayPoint = (currentWayPoint + 1) % waypoints.Length;
    }

    private void FixedUpdate()
    {
        if (hp.isAlive)
        {
            sight.origin = transform.position;
            sight.direction = transform.forward;

            //if (Physics.SphereCast(sight, sphereRadius, out rayHit, sphereDis, layerMask))
            if (Physics.Raycast(sight, out rayHit, maxDistance, layerMask))
            {
                Debug.DrawLine(sight.origin, rayHit.point, Color.red);
                //if (!isPlayerDetected && rayHit.collider.CompareTag("PlayerBody"))
                if (!isPlayerDetected && rayHit.collider.CompareTag("Player"))
                {
                    isPlayerDetected = true;
                    agent.autoBraking = true;
                    agent.stoppingDistance = 70f;
                    foundTextAnim.SetTrigger("FoundPlayer");
                    agent.SetDestination(player.position);
                }
            }


            if (isPlayerDetected)
            {
                float playerDis = Vector3.Distance(player.position, transform.position);
                //float playerDis = agent.remainingDistance;

                //Debug.Log("remainingDistance To player: " + agent.remainingDistance);
                //Debug.Log("playerDis To player: " + playerDis);
                //Debug.Log("stoping To player: " + agent.stoppingDistance);
                //Debug.Log("player pos: " + player.position);

                if (playerDis <= agent.stoppingDistance)
                    transform.LookAt(player);

                else if (playerDis < lostDistance)
                    //Debug.Log("middle: " + agent.stoppingDistance);
                    agent.speed = 30f;

                else // playerDis > lostDistance
                {
                    lostTextAnim.SetTrigger("LostPlayer");
                    agent.stoppingDistance = 5f;
                    agent.speed = 25f;
                    isPlayerDetected = false;
                    agent.autoBraking = false;
                    GotoNextPoint();
                }

                agent.SetDestination(player.position);
            }
        }
    }
}


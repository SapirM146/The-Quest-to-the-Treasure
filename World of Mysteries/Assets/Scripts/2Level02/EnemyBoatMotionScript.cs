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
    public bool isPlayerDetected;
    float maxDistance = 200f;
    float lostDistance;
    bool playerInPursueRange;
    LayerMask layerMask = 1 << 10;     // Bit shift the index of the layer (10) to get a bit mask

    public Animator foundTextAnim;
    public Animator lostTextAnim;


    private void Start()
    {
        isPlayerDetected = false;
        playerInPursueRange = false;
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

            if (Physics.Raycast(sight, out rayHit, maxDistance, layerMask))
            {
                Debug.DrawLine(sight.origin, rayHit.point, Color.red); // shows red line to player boat in scene
                if (!isPlayerDetected && rayHit.collider.CompareTag("Player"))
                    foundPlayer();
            }

            Transform origin = hp.damageOrigin;
            if (!isPlayerDetected && origin != null && origin == player)
                foundPlayer();

            if (isPlayerDetected)
            {
                float playerDis = Vector3.Distance(player.position, transform.position);

                if (playerDis <= agent.stoppingDistance)
                    transform.LookAt(player);

                else if (playerDis < lostDistance)
                {
                    agent.speed = 30f;
                    playerInPursueRange = true;
                }

                else if (playerInPursueRange)// playerDis > lostDistance
                {
                    playerInPursueRange = false;
                    lostPlayer();
                }
                agent.SetDestination(player.position);
            }
        }
    }

    public void foundPlayer()
    {
        isPlayerDetected = true;
        agent.autoBraking = true;
        agent.stoppingDistance = 70f;
        foundTextAnim.SetTrigger("FoundLostPlayer");
        agent.SetDestination(player.position);
    }

    public void lostPlayer()
    {
        hp.damageOrigin = null;
        lostTextAnim.SetTrigger("FoundLostPlayer");
        agent.stoppingDistance = 5f;
        agent.speed = 25f;
        isPlayerDetected = false;
        agent.autoBraking = false;
        GotoNextPoint();
    }
}


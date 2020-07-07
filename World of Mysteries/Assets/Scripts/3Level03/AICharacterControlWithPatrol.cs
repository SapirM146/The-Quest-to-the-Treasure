using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControlWithPatrol : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        EnemyHPScript hp;
        public FoundPlayerTrigger foundPlayerTrigger;
        public Transform[] waypoints;
        int currentWayPoint;
        public float walkSpeed;
        public float runSpeed;
        Animator animator;
        public SlashHitTrigger slash;


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            hp = GetComponent<EnemyHPScript>();
            character = GetComponent<ThirdPersonCharacter>();
            animator = GetComponent<Animator>();

            agent.updateRotation = false;
	        agent.updatePosition = true;
            walkSpeed = agent.speed;
            GotoNextPoint();
        }


        private void Update()
        {
            if (!hp.isAlive)
            {
                agent.isStopped = true;
                if(slash != null)
                    slash.GetComponent<CapsuleCollider>().enabled = false;
            }
            else
            {
                if (target == null)
                    GotoNextPoint();

                else // target != null
                {
                    agent.SetDestination(target.position);
                    
                    if (agent.remainingDistance > agent.stoppingDistance)
                    {
                        character.Move(agent.desiredVelocity, false, false);
                    }
                    else
                    {
                        agent.velocity = Vector3.zero;
                        character.Move(Vector3.zero, false, false);

                        if (!foundPlayerTrigger.foundPlayer)
                            GotoNextPoint();

                        else if (Vector3.Distance(transform.position, foundPlayerTrigger.player.position) <= 
                            agent.stoppingDistance + 0.3 && animator != null)
                        {
                            transform.LookAt(agent.destination);
                            animator.SetTrigger("Attack");
                        }
                    }
                }
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        void GotoNextPoint()
        {
            if (waypoints.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            SetTarget(waypoints[currentWayPoint]);
            agent.SetDestination(waypoints[currentWayPoint].position);

            currentWayPoint = (currentWayPoint + 1) % waypoints.Length;
        }
    }
}

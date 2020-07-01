using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class AIBossCharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }  // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        EnemyHPScript hp;
        public float walkSpeed;
        public float runSpeed;
        public float walkAcceleration;
        public float runAcceleration;
        Animator animator;
        public SlashHitTrigger slash;
        public float timerMin = 10f;
        public float timerMax = 20f;
        public float RunningDurration = 10f;
        bool isRunning = false;



        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            hp = GetComponent<EnemyHPScript>();
            character = GetComponent<ThirdPersonCharacter>();
            animator = GetComponent<Animator>();

            agent.updateRotation = false;
            agent.updatePosition = true;
            //slash.damage = 0;
        }


        private void Update()
        {
            if (!hp.isAlive)
            {
                StopCoroutine(ChargeAtPlayer());
                agent.isStopped = true;
                slash.GetComponent<CapsuleCollider>().enabled = false;
            }
            else
            {
                if (!isRunning)
                    StartCoroutine(ChargeAtPlayer());

                if (target == null)
                    return;

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

                        if (Vector3.Distance(transform.position, target.position) <=
                            agent.stoppingDistance + 0.5 && animator != null)
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

        IEnumerator ChargeAtPlayer()
        {
            isRunning = true;
            float time = Random.Range(timerMin, timerMax);
            yield return new WaitForSeconds(time);
            agent.speed = runSpeed;
            agent.acceleration = runAcceleration;
            animator.speed = 2f;
            Invoke("stopRunning", RunningDurration);
        }

        void stopRunning()
        {
            isRunning = false;
            animator.speed = 1f;
            agent.speed = walkSpeed;
            agent.acceleration = walkAcceleration;
        }
    }
}

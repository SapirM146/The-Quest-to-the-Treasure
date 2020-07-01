using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        EnemyHPScript hp;


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            hp = GetComponent<EnemyHPScript>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (!hp.isAlive)
            {
                //agent.velocity = Vector3.zero;
                agent.isStopped = true;
            }
            else
            {
                //if (target == null)
                //{
                //    agent.velocity = Vector3.zero;
                //    character.Move(Vector3.zero, false, false);
                //}

                //else // target != null
                if (target != null)
                {
                    if (agent.isStopped)
                        agent.isStopped = false;
                    Debug.DrawLine(agent.destination, new Vector3(
                        agent.destination.x, agent.destination.y + 1f, agent.destination.z), Color.red);

                    Debug.DrawLine(transform.position, target.position, Color.blue);

                    agent.SetDestination(target.position);
                }

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    character.Move(agent.desiredVelocity, false, false);
                }
                else
                {
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                    character.Move(Vector3.zero, false, false);
                }
            }
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}

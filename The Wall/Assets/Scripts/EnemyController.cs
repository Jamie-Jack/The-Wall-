using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Com.NewVisionGamesStudio.TheWall
{
    public class EnemyController : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Slider gateHealth;
        public bool doOnce = true;



        void UpdateDestination()
        {
            agent.SetDestination(GameObject.FindGameObjectWithTag("Gate").transform.position);
        }
        void Update()
        {
            if (doOnce)
            {
                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            doOnce = false;
                            InvokeRepeating("EnemyAttackGate", 1, 1);
                        }
                    }
                }
            }
        }

        void Start()
        {
            UpdateDestination();
            gateHealth = GameObject.FindGameObjectWithTag("GateHP").GetComponent<Slider>();
        }

        void EnemyAttackGate()
        {
            gateHealth.value -= 5;
            if (gateHealth.value <= 0)
            {
                // print("YOU LOSE");
                EventSystem.current.GetComponent<GameMode>().GameOver();
            }

        }
    }
}

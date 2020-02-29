using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Com.NewVisionGamesStudio.TheWall
{
    public class EnemyPathScript : MonoBehaviour
    {

        public LineRenderer line; //to hold the line Renderer
        public Transform target; //to hold the transform of the target
        public NavMeshAgent agent; //to hold the agent of this gameObject

        void Start()
        {
            line = GetComponent<LineRenderer>(); //get the line renderer
            agent = GetComponent<NavMeshAgent>(); //get the agent
            getPath();
        }

        void getPath()
        {
            line.SetPosition(0, transform.position); //set the line's origin

            agent.SetDestination(target.position); //create the path
            StartCoroutine(DrawPath());



            //agent.isStopped = true;//add this if you don't want to move the agent
        }

        IEnumerator DrawPath()
        {
            yield return new WaitForEndOfFrame();
            //if (path.corners.Length < 2) //if the path has 1 or no corners, there is no need
            //return;

            line.positionCount = agent.path.corners.Length; //set the array of positions to the amount of corners
            //print(agent.path.corners.Length);
            for (var i = 1; i < agent.path.corners.Length; i++)
            {

                var linex = agent.path.corners[i].x;
                var linez = agent.path.corners[i].z;
                var height = agent.path.corners[i].y;
                line.SetPosition(i, new Vector3(linex, height, linez)); //go through each corner and set that to the line renderer's position
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
namespace Com.NewVisionGamesStudio.TheWall
{
    public class GameMode : MonoBehaviour
    {
        public bool DoOnce = false;
        public Rigidbody rb;
        public Collider bc;
        public GameObject MainCam, Player, NewTargetPoint;
        public void GameOver()
        {

            if (DoOnce == false)
            {
                DoOnce = true;
                rb.isKinematic = false;
                bc.enabled = true;
                print("Game Over");
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().enabled = false;
                MainCam.GetComponent<Camera>().enabled = true;
                MainCam.GetComponent<Animator>().SetTrigger("Death_Cam");

                var objects = GameObject.FindGameObjectsWithTag("Enemy");
                var objectCount = objects.Length;
                foreach (var obj in objects)
                {
                    obj.GetComponent<NavMeshAgent>().destination = NewTargetPoint.transform.position;
                }
            }

        }
        public void RetryButton()
        {
            SceneManager.LoadScene(0);
        }
    }
}
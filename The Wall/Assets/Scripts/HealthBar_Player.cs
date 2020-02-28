using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Com.NewVisionGamesStudio.TheWall
{
    public class HealthBar_Player : MonoBehaviour 
    {
        public Slider HealthBar;
        public Camera MainCam;
        public GameObject Respawn;

        private void Start()
        {
            
            
            print(GameObject.FindGameObjectWithTag("Respawn Button"));
            MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            Respawn = GameObject.FindGameObjectWithTag("Respawn Button");
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                HealthBar.value -= 25f;
            if(HealthBar.value <= 0)
                {

                   MainCam.enabled = true;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Respawn.GetComponent<Canvas>().enabled = true;
                    Destroy(this.gameObject);
                   
                   
                }
            }
        }


    }
}
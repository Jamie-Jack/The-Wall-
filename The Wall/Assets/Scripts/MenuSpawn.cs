using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.NewVisionGamesStudio.TheWall
{
    public class MenuSpawn : MonoBehaviour
    {
        int maxEnemy = 10;
        int enemyCount = 0;

        float time = 0.0f;
        float lap = 10.0f;

        Ray ray;
        RaycastHit hit;
        public GameObject prefab;
        // Use this for initialization
      

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
             if (time >= lap)
             {
                enemyCount = 0;
                time = 0;
             }
            if (enemyCount >= maxEnemy) return;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    GameObject obj = Instantiate(prefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
                    enemyCount++;
                }
               


            }
        }
    }
}
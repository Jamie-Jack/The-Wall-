
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.NewVisionGamesStudio.TheWall
{
    public class EnemySpawner : MonoBehaviour
    {

        public GameObject enemy;
        public bool stopSpawning = false;
        public float spawnTime;
        public float spawnDelay;
        private int spawnCount;

        // Use this for initialization
        void Start()
        {
            InvokeRepeating("SpawnObject", spawnTime, spawnDelay);

        }

        public void SpawnObject()
        {
            spawnCount++;
            print(spawnCount);

            if (spawnCount <= 20)
            {
                Instantiate(enemy, transform.position, transform.rotation);

            }
            else
            {
                CancelInvoke("SpawnObject");
            }
            if (stopSpawning)
            {
                CancelInvoke("SpawnObject");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.NewVisionGamesStudio.TheWall
{
    public class Turret : MonoBehaviour
    {
        public Transform target;

        [Header("Attributes")]

        public float range = 15f;
        public float fireRate = 1f;
        private float fireCountdown = 0f;

        [Header("Unity Setup")]
        public float turnSpeed = 10f;
        public Transform gunRotation;
        public string enemyTag = "Enemy";

        public GameObject bulletPrefab;
        public Transform firePoint;



        void Start()
        {
            InvokeRepeating("updateTarget", 0f, 0.5f);

        }
        void updateTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemey = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemey < shortestDistance)
                {
                    shortestDistance = distanceToEnemey;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;

            }
            else
            {
                target = null;
            }
        }


        void Update()
        {
            if (target == null)
                return;


            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(gunRotation.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            gunRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;

            }

            fireCountdown -= Time.deltaTime;
        }
        void Shoot()
        {
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            TurretBullet bullet = bulletGO.GetComponent<TurretBullet>();

            if (bullet != null)
                bullet.Seek(target);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}

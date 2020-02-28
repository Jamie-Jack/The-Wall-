using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Com.NewVisionGamesStudio.TheWall
{
    public class TurretBullet : MonoBehaviour
    {
        private Transform target;

        public float speed = 70f;
        public GameObject impactEffect;
        public GameObject Coin;

        public void Seek(Transform _target)
        {
            target = _target;
        }

        void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        }

        void HitTarget()
        {
            GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            target.GetComponentInChildren<Slider>().value -= 90f;
            if (target.GetComponentInChildren<Slider>().value <= 0)
            {
                Destroy(target.transform.gameObject);
                Instantiate(Coin, target.transform.position, transform.rotation);
            }

            Destroy(effectIns, 1.25f);

            Destroy(gameObject);
        }
    }
}

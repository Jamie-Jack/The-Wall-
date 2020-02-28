using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Com.NewVisionGamesStudio.TheWall
{
    public class Weapon : MonoBehaviour
    {
        public Gun[] loadout;
        public Transform weaponParent;
        public GameObject bulletholePrefab;
        public GameObject Coin;
        public LayerMask canBeShot;
        public LayerMask Enemy;

        private float currentCooldown;
        private int CurrentIndex;
        private GameObject currentEquipment;

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);

            if (currentEquipment != null)
            {
                Aim(Input.GetMouseButton(1));

                if (Input.GetMouseButtonDown(0)&& currentCooldown <=0)
                {
                    Shoot();
                }

                // weapon position elasticity
                currentEquipment.transform.localPosition = Vector3.Lerp(currentEquipment.transform.localPosition, Vector3.zero, Time.deltaTime * 4f);

                //cooldown 
                if (currentCooldown > 0) currentCooldown -= Time.deltaTime;
            }
        }

        void Equip(int p_ind)
        {
            if (currentEquipment != null) Destroy(currentEquipment);

            CurrentIndex = p_ind;

            GameObject t_newEquipment = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
            t_newEquipment.transform.localPosition = Vector3.zero;
            t_newEquipment.transform.localEulerAngles = Vector3.zero;

            currentEquipment = t_newEquipment;
        }

        void Aim(bool p_isAiming)
        {
            Transform t_anchor = currentEquipment.transform.Find("Anchor");
            Transform t_state_ads = currentEquipment.transform.Find("States/ADS");
            Transform t_state_hip = currentEquipment.transform.Find("States/Hip");

            if (p_isAiming)
            {
                t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.position, Time.deltaTime * loadout[CurrentIndex].aimSpeed);
            }
            else
            {
                t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.position, Time.deltaTime * loadout[CurrentIndex].aimSpeed);
            }
        }
        void Shoot()
        {
            Transform t_spawn = transform.Find("Cameras/Normal Camera");

            //bloom 
            Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000f;
            t_bloom += Random.Range(-loadout[CurrentIndex].bloom, loadout[CurrentIndex].bloom) * t_spawn.up;
            t_bloom += Random.Range(-loadout[CurrentIndex].bloom, loadout[CurrentIndex].bloom) * t_spawn.right;
            t_bloom -= t_spawn.position;
            t_bloom.Normalize();

            //raycast
            RaycastHit t_hit = new RaycastHit();
            if (Physics.Raycast(t_spawn.position, t_bloom, out t_hit, 1000f, canBeShot))
            {
                GameObject t_newHole = Instantiate(bulletholePrefab, t_hit.point + t_hit.normal * 0.001f, Quaternion.identity) as GameObject;
                t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
                Destroy(t_newHole, 0.75f);
                if (Physics.Raycast(t_spawn.position, t_bloom, out t_hit, 1000f, Enemy))
                {
                    t_hit.transform.GetComponentInChildren<Slider>().value -= loadout[CurrentIndex].damage;
                    if (t_hit.transform.GetComponentInChildren<Slider>().value <= 0)
                    {
                        Destroy(t_hit.transform.gameObject);
                        Instantiate(Coin, t_hit.point, transform.rotation);
                    }
                }
            }

            //gun effects
            currentEquipment.transform.Rotate(-loadout[CurrentIndex].recoil, 0, 0);
            currentEquipment.transform.position -= currentEquipment.transform.forward * loadout[CurrentIndex].kickback;

            //cooldown
            currentCooldown = loadout[CurrentIndex].firerate;
        }
    }
}
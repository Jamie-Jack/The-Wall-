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

        private bool isReloading;

        void Start()
        {
            foreach (Gun a in loadout) a.Initialize();
            Equip(0);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) Equip(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) Equip(2);

            if (currentEquipment != null)
            {
                Aim(Input.GetMouseButton(1));

                if (loadout[CurrentIndex].burst != 1)
                {
                    if (Input.GetMouseButtonDown(0) && currentCooldown <= 0)
                    {
                        if (loadout[CurrentIndex].FireBullet()) Shoot();
                        else StartCoroutine(Reload(loadout[CurrentIndex].reload));
                    }
                }
                else
                {
                    if (Input.GetMouseButton(0) && currentCooldown <= 0)
                    {
                        if (loadout[CurrentIndex].FireBullet()) Shoot();
                        else StartCoroutine(Reload(loadout[CurrentIndex].reload));
                    }
                }

                if (Input.GetKeyDown(KeyCode.R)) StartCoroutine(Reload(loadout[CurrentIndex].reload));

                // weapon position elasticity
                currentEquipment.transform.localPosition = Vector3.Lerp(currentEquipment.transform.localPosition, Vector3.zero, Time.deltaTime * 4f);

                //cooldown 
                if (currentCooldown > 0) currentCooldown -= Time.deltaTime;
            }
        }

        // Private Methods 
         IEnumerator Reload(float p_wait)
        {
            isReloading = true;
            currentEquipment.SetActive(false);

            yield return new WaitForSeconds(p_wait);

            loadout[CurrentIndex].Reload();
            currentEquipment.SetActive(true);

            isReloading = false;
        }

        void Equip(int p_ind)
        {
            if (currentEquipment != null)
            {
               if(isReloading) StopCoroutine("Reload");
                Destroy(currentEquipment);
            }

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
        public void RefreshAmmo (Text p_text)
        {
            int t_clip = loadout[CurrentIndex].GetClip();
            int t_stash = loadout[CurrentIndex].GetStash();

            p_text.text =" Ammo: " + t_clip.ToString("D2") + " / " + t_stash.ToString("D2");
        }
    }
}
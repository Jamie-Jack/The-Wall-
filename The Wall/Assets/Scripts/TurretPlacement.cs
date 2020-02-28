using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Com.NewVisionGamesStudio.TheWall
{
    public class TurretPlacement : MonoBehaviour
    {
        [SerializeField]
        public GameObject placeableObjectPrefab;
        public KeyCode newObjectHotkey = KeyCode.A;
        public Camera normalCam;
        public GameObject gun,gunBase;

        public Material[] matsBase, matsGunBarrel, greenMatsBase,greenMatsGunBarrel;
        public Material greenmat;
      



        private GameObject currentPlaceableObject;
        private float mousewheelRotation;
        public PlayerVariables PVs;

         void Start()
        {
            PVs = EventSystem.current.GetComponent<PlayerVariables>();
           
        }

        private void Update()
        {
            HandleNewObjectHotKey();
            if (currentPlaceableObject != null)
            {
                MoveCurrrentPlaceableObjectToMouse();
                RotateFromMouseWheel();
                ReleaseIfClicked();
            }
        }

        private void ReleaseIfClicked()
        {
            if (Input.GetMouseButtonDown(0))
            {

                currentPlaceableObject.GetComponent<MeshCollider>().enabled = true;

                gun.GetComponent<Renderer>().materials = matsGunBarrel;
                gunBase.GetComponent<Renderer>().materials = matsBase ;

                currentPlaceableObject = null;
                EventSystem.current.GetComponent<PlayerVariables>().UpdateCoins(-10);
               
            }
        }

        private void RotateFromMouseWheel()
        {
            mousewheelRotation = Input.mouseScrollDelta.y;
            currentPlaceableObject.transform.Rotate(Vector3.up, mousewheelRotation * 10f);
        }
        private void MoveCurrrentPlaceableObjectToMouse()
        {
            Ray ray = normalCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                currentPlaceableObject.transform.position = hitInfo.point;
                currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
        }
        private void HandleNewObjectHotKey()
        {
            if (Input.GetKeyDown(newObjectHotkey))
            {
                if (currentPlaceableObject == null && PVs.coinInt >=10)
                {
                    currentPlaceableObject = Instantiate(placeableObjectPrefab);

                    gun = currentPlaceableObject.transform.Find("Gun Rotate").transform.Find("Gun").gameObject;
                    gunBase = currentPlaceableObject.transform.Find("Base").gameObject;
                    print(gun.name);



                    matsGunBarrel = gun.GetComponent<Renderer>().materials;
                    matsBase = gunBase.GetComponent<Renderer>().materials;
                    greenMatsBase = new Material[matsBase.Length];
                    greenMatsGunBarrel = new Material[matsGunBarrel.Length];


                    for(int i = 0; i < greenMatsGunBarrel.Length; i++)
                    {
                        greenMatsGunBarrel[i] = greenmat;

                    }

                    for (int i = 0; i < greenMatsBase.Length; i++)
                    {
                        greenMatsBase[i] = greenmat;

                    }

                    gun.GetComponent<Renderer>().materials = greenMatsGunBarrel;
                    gunBase.GetComponent<Renderer>().materials = greenMatsBase;


                }
                else
                {
                    Destroy(currentPlaceableObject);

                }
            }
        }
    }
}

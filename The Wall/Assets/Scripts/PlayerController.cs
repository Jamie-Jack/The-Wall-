using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.NewVisionGamesStudio.TheWall
{


    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float sprintModifier;
        public float jumpForce;
        public float LengthOfSlide;
        public float slideModifier;
        public Camera normalCam;

        public Transform weaponParent;
        public Transform groundDetector;
        public LayerMask ground;

        private Vector3 weaponParentOrigin;
        private Vector3 weaponParentCurrentPosition;
        private Vector3 targetWeaponBobPosition;

        private float movementCounter;
        private float idleCounter;
        
        private Rigidbody rig;
        private float baseFOV;
        private float sprintFOVModifier = 1.5f;
        private Vector3 origin;


        private bool sliding;
        private float slide_time;
        private Vector3 slide_dir;
        
       

        private void Start()
        {
            baseFOV = normalCam.fieldOfView;
            origin = normalCam.transform.localPosition;
            Camera.main.enabled = false;
            rig = GetComponent<Rigidbody>();
            weaponParentOrigin = weaponParent.localPosition;
            weaponParentCurrentPosition = weaponParentOrigin;
        }


        void FixedUpdate()
        {

            float T_hmove = Input.GetAxis("Horizontal");
            float T_vmove = Input.GetAxis("Vertical");


            // Inputs
            bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            bool jump = Input.GetKeyDown(KeyCode.Space);
            bool slide = Input.GetKey(KeyCode.C);


            // States 
            bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
            bool isJumping = jump && isGrounded;
            bool isSprinting = sprint && T_vmove > 0 && !isJumping && isGrounded;
            bool isSliding = isSprinting && slide && !sliding;

            // Jump
            if (isJumping)
            {
                rig.AddForce(Vector3.up * jumpForce);
            }

            // Headbob
            if (T_hmove == 0 && T_vmove == 0)
            {
                HeadBob(idleCounter, 0.025f, 0.025f);
                idleCounter += Time.deltaTime;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
            }

            else if (!isSprinting)
            {
                HeadBob(movementCounter, 0.035f, 0.035f);
                movementCounter += Time.deltaTime * 3f;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 5f);
            }
            else
            {
                HeadBob(movementCounter, 0.15f, 0.075f);
                movementCounter += Time.deltaTime * 7f;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10f);
            }

            // Walk and Slide
            Vector3 T_Direction = Vector3.zero;
            float t_adjustedSpeed = speed;
            if (!sliding)
            {

                T_Direction = new Vector3(T_hmove, 0, T_vmove);
                T_Direction.Normalize();
                T_Direction = transform.TransformDirection(T_Direction);

                if (isSprinting) t_adjustedSpeed *= sprintModifier;

            }
            else
            {
                T_Direction = slide_dir;
                t_adjustedSpeed *= slideModifier;
                slide_time -= Time.deltaTime;
                if (slide_time <= 0)
                {
                    sliding = false;
                    weaponParentCurrentPosition += Vector3.up * 0.5f;
                }
            }

            Vector3 t_targetVelocity = T_Direction * t_adjustedSpeed * Time.deltaTime;
            t_targetVelocity.y = rig.velocity.y;
            rig.velocity = t_targetVelocity;

            // Sliding Stuff
            if (isSliding)
            {
                sliding = true;
                slide_dir = T_Direction;
                slide_time = LengthOfSlide;
                // cam adjustment 
             weaponParentCurrentPosition += Vector3.down * 0.5f;

            }


            // Field of View Stuff & slide Cam
            if (sliding)
            {
                normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier * 1.25f, Time.deltaTime * 8f);
                normalCam.transform.localPosition = Vector3.Lerp(normalCam.transform.localPosition, origin + Vector3.down * 0.5f, Time.deltaTime * 6f);
            }
           else
            {
                if (isSprinting) { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f); }
                else { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f); }

                normalCam.transform.localPosition = Vector3.Lerp(normalCam.transform.localPosition, origin, Time.deltaTime * 6f);
            }
        
        }

        // Headbob Stuff
        void HeadBob(float p_z, float p_x_intensity, float p_y_Intensity)
        {
           targetWeaponBobPosition = weaponParentCurrentPosition + new Vector3(Mathf.Cos(p_z) * p_x_intensity, Mathf.Sin(p_z * 2) * p_y_Intensity,0);
        }
    }
}
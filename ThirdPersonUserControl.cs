using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

using Pose = Thalmic.Myo.Pose;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        //my own code
        public GameObject myo = null;
        private Pose lastPose = Pose.Unknown;
        private Quaternion antiYaw = Quaternion.identity;
        private float referenceRoll = 0.0f;
        private Vector3 referenceZeroRoll;

        private ThalmicMyo thalmicMyo;

        private void Start()
        {

            thalmicMyo = myo.GetComponent<ThalmicMyo>();

            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();

            //antiYaw = Quaternion.FromToRotation(new Vector3(myo.transform.forward.x, 0, myo.transform.forward.z), new Vector3(0, 0, 1));
            //referenceZeroRoll = computeZeroRollVector(myo.transform.forward);
            //referenceRoll = rollFromZero(referenceZeroRoll, myo.transform.forward, myo.transform.up);
        }


        private void Update()
        {

            //my own code
            //ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
            //if (thalmicMyo.pose != lastPose) {
                //lastPose = thalmicMyo.pose;
                if(thalmicMyo.pose == Pose.Fist) {
                    if (!m_Jump) {
                        m_Jump = true; 
                    }
                }
            //}
            
            /**
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            **/
        }

        float rollFromZero(Vector3 zeroRoll, Vector3 forward, Vector3 up) {
            float cosine = Vector3.Dot(up, zeroRoll);
            Vector3 cp = Vector3.Cross(up, zeroRoll);
            float directionCosine = Vector3.Dot(forward, cp);
            float sign = directionCosine < 0.0f ? 1.0f : -1.0f;

            return sign * Mathf.Rad2Deg * Mathf.Acos(cosine);
        }

        Vector3 computeZeroRollVector(Vector3 forward) {
            Vector3 antigravity = Vector3.up;
            Vector3 m = Vector3.Cross(myo.transform.forward, antigravity);
            Vector3 roll = Vector3.Cross(m, myo.transform.forward);

            return roll.normalized;
        }

        float normalizeAngle(float angle) {
            if(angle > 180.0f) {
                return angle - 360.0f;
            }
            if(angle < -180.0f) {
                return angle + 360.0f;
            }
            return angle;
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal"); // a and d input. goes from -1 ~ 1
            //float v = CrossPlatformInputManager.GetAxis("Vertical"); // w and s input. goes from -1 ~ 1

            //my own code
            //ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

            //Vector3 zeroRoll = computeZeroRollVector(myo.transform.forward);
            //float roll = rollFromZero(zeroRoll, myo.transform.forward, myo.transform.up);
            //float relativeRoll = normalizeAngle(roll - referenceRoll);
            //Quaternion antiRoll = Quaternion.AngleAxis(relativeRoll, myo.transform.forward);

            //float h = (antiYaw * antiRoll * Quaternion.LookRotation(myo.transform.forward)).y;
            //h -= (float)0.3;
            //h *= (float)0.1;

            float v = thalmicMyo.gyroscope.y / 125;

            //float v = 0.5f; //android version purposes

            if (v > 1) {
                v = 1;
            }  else if(Math.Abs(v) < 0.05) {
                v = 0;
            } else if (v < 0) {
                v = 1;
            }

            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
    }
}

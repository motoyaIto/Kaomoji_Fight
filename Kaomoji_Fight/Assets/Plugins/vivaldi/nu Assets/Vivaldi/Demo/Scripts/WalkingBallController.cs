using UnityEngine;
using System.Collections;
namespace ch.sycoforge.Vivaldi.Demo
{
    [RequireComponent(typeof(Rigidbody))]
    public class WalkingBallController : MonoBehaviour
    {
        //---------------------------
        // Exposed Fields
        //---------------------------
        public VivaldiComposition RollingSFX;
        public float Speed = 16;



        //---------------------------
        // Internal Fields
        //---------------------------
        private Rigidbody body;
        private float maxSpeed = 4;
        private float speedFactor;
        private CompositionControl rollControl;

        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";

        //---------------------------
        // Methods
        //---------------------------

        #region --- Unity Methods ---
        private void Start()
        {
            body = GetComponent<Rigidbody>();

            rollControl = RollingSFX.Play();
        }
    
        private void Update()
        {
            ProcessInput();

            ProcessPhysics();
            ProcessSFX();
        }


        #endregion

        private void ProcessInput()
        {
            float vertical = Input.GetAxis(VERTICAL);
            float horizontal = Input.GetAxis(HORIZONTAL);

            Vector3 velocity = new Vector3(horizontal, 0, vertical) * Speed * Time.deltaTime;

            body.AddForce(velocity, ForceMode.VelocityChange);
        }

        private void ProcessSFX()
        {
            if(rollControl != null && rollControl.IsValid)
            {
                // The faster the marbel rolls, the louder the SFX is
                rollControl.Volume = speedFactor;
            }
        }

        private void ProcessPhysics()
        {
            float currentSpeed = body.velocity.magnitude;
            if(maxSpeed < currentSpeed)
            {
                maxSpeed = currentSpeed;
            }

            // Normalize [0..1]
            speedFactor = Mathf.Clamp01(currentSpeed / maxSpeed);
        }
    }
}

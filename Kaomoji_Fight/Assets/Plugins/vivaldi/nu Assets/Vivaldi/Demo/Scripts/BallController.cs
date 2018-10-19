using UnityEngine;
using System.Collections;
namespace ch.sycoforge.Vivaldi.Demo
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallController : MonoBehaviour
    {
        //---------------------------
        // Exposed Fields
        //---------------------------
        public VivaldiComposition RollingSFX;
        public VivaldiComposition HitSFX;

        [Range(0.0f, 1.0f)]
        public float HitVolume = 0.3f;


        //---------------------------
        // Internal Fields
        //---------------------------
        private Rigidbody body;
        private float maxSpeed = 4;
        private float speedFactor;
        private CompositionControl rollControl;

        private const string WALLPREFIX = "Wall";


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
            ProcessPhysics();
            ProcessSFX();
        }

        private void OnCollisionEnter(Collision collison)
        {
            if(collison.gameObject.name.StartsWith(WALLPREFIX))
            {
                CompositionControl hitControl = HitSFX.Play();
                
                if(hitControl.IsValid)
                {
                    // The faster the marbel hits the wall, the louder the hit SFX is
                    hitControl.Volume = speedFactor * HitVolume;
                }
            }
        }

        #endregion


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

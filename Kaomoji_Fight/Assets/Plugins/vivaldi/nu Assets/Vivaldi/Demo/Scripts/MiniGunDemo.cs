using ch.sycoforge.Vivaldi;
using UnityEngine;

namespace ch.sycoforge.Vivaldi.Demo
{
    public class MiniGunDemo : MonoBehaviour
    {
        //---------------------------
        // Fields
        //---------------------------
        public VivaldiComposition MinigunSFX;
        private CompositionControl control;

        //---------------------------
        // Methods
        //---------------------------
        public void BeginFire()
        {
            control = MinigunSFX.Play();
            control.OnLoopEnter += control_OnLoopEnter;
            control.OnLoopPass += control_OnLoopPass;
        }

        public void EndFire()
        {
            if (control != null && control.IsValid)
            {
                control.GoTo(control.Composition.CuePoints[0]);
            }
        }

        void control_OnLoopPass(CompositionControl controlHandle, SFXRuntimeLoop loop)
        {
            Debug.Log("OnLoopPass: " + loop.PassCount);
        }

        void control_OnLoopEnter(CompositionControl controlHandle, SFXRuntimeLoop loop)
        {
            Debug.Log("OnLoopEnter: " + loop);
        }
    }
}

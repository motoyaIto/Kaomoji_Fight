using ch.sycoforge.Unity.Pooling;
using System;
using UnityEngine;

namespace ch.sycoforge.Vivaldi.Demo
{
    [RequireComponent(typeof(VivaldiComposer))]
    public class ViewController : MonoBehaviour
    {
        //---------------------------
        // Exposed Fields
        //---------------------------
        public ProgressBar ProgressBarSFXPool;
        public ProgressBar ProgressBarCompPool;

        //---------------------------
        // Fields
        //---------------------------
        private VivaldiComposer composer;

        //---------------------------
        // Methods
        //---------------------------
        private void Start()
        {
            composer = GetComponent<VivaldiComposer>();
        }

        private void Update()
        {
            ProgressBarSFXPool.Progress = GetPoolLevel(composer.PoolSFX);
            ProgressBarCompPool.Progress = GetPoolLevel(composer.PoolComp);
        }

        private float GetPoolLevel(GenericPool pool)
        {
            float poolLevel = pool.ReadyCount / (float)Math.Max(pool.AbsoluteSize, 1);

            return poolLevel;
        }
    }
}

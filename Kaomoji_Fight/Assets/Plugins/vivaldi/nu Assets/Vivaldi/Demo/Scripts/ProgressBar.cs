using UnityEngine;

namespace ch.sycoforge.Vivaldi.Demo
{
    [RequireComponent(typeof(RectTransform))]
    public class ProgressBar : MonoBehaviour
    {
        //---------------------------
        // Exposed Fields
        //---------------------------
        [Range(0.0f, 1.0f)]
        public float Progress;

        //---------------------------
        // Fields
        //---------------------------
        private RectTransform parent;
        private RectTransform child;
        private const string ChildName = "Bar";

        //---------------------------
        // Methods
        //---------------------------
        private void Start()
        {
            parent = this.transform as RectTransform;
            child = this.transform.Find(ChildName) as RectTransform;
        }

        private void Update()
        {
            float parentWidth = parent.sizeDelta.x;
            child.sizeDelta = new Vector2(Progress * parentWidth, child.sizeDelta.y);
        }
    }
}

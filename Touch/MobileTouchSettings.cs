using System;
using UnityEngine;

namespace BT.Input.Touch
{
    [Serializable]
    public class MobileTouchSettings : ScriptableObject
    {
        [SerializeField]
        [Range(0f, 1f)]
        private float swipeTolerance = 0.85f;
        [SerializeField]
        [Range(0f, 1f)]
        private float swipeLength = 0.1f;
        [SerializeField]
        [Range(0f, 1f)]
        private float tapTime = 0.5f;

        public float SwipeTolerance => swipeTolerance;
        public float SwipeLength => swipeLength;
        public float TapTime => tapTime;
        
        public int GetSwipeLengthInPixels()
        {
            return Mathf.CeilToInt(SwipeLength * Screen.dpi);
        }
    }
}
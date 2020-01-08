using UnityEngine;

namespace BT.Input.Touch
{
    public struct Touch
    {
        public TouchInfo TouchInfo { get; set; }
        public GestureType GestureType { get; set; }
        public SwipeDirection SwipeDirection { get; set; }
        public float SwipeLength { get; set; }
        public Vector2 SwipeVector { get; set; }

        public bool IsOverRectTransform(RectTransform tr, Camera camera = null)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(tr, TouchInfo.Position, camera);
        }
    }
}
using System.Linq;
using UnityEngine;

namespace BT.Input.Touch
{
    public struct Pinch
    {
        public TouchInfo[] TouchInfos { get; set; }
        public GestureType GestureType { get; set; }
        public Vector2 PinchVector { get; set; }

        public bool IsOverRectTransform(RectTransform tr, Camera camera = null)
        {
            return TouchInfos.All(i => RectTransformUtility.RectangleContainsScreenPoint(tr, i.Position, camera));
        }
    }
}
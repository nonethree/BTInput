using UnityEngine;

namespace BT.Input.Touch
{
    public struct TouchInfo
    {
        public int FingerId { get; set; }
        public int TapCount { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 PositionDelta { get; set; }
        public float TimeDelta { get; set; }
        public TouchPhase Phase { get; set; }
        public float StartTime { get; set; }
        public float ActionTime { get; set; }
    }
}
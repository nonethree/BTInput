using UnityEngine;

namespace BT.Input.Touch
{
    public class RealTouchInput : TouchInput
    {
        public override int TouchCount()
        {
            return UnityEngine.Input.touchCount;
        }

        public override TouchInfo GetTouch(int touchIndex)
        {
            UnityEngine.Touch inputTouch = UnityEngine.Input.GetTouch(touchIndex);
            return new TouchInfo()
            {
                FingerId = inputTouch.fingerId,
                TapCount = inputTouch.tapCount,
                Position = inputTouch.position,
                PositionDelta = inputTouch.deltaPosition,
                TimeDelta = inputTouch.deltaTime,
                Phase = inputTouch.phase,
            };
        }
    }
}
using UnityEngine;

namespace BT.Input.Touch
{
    public class MouseTouchInput : TouchInput
    {
        #region Private members

        private Vector2 oldMousePosition = new Vector2();
        private int tapCount;
        private float startActionTime;
        private float deltaTime;
        private float tapeTime;

        #endregion Private members

        #region Public methods

        public override int TouchCount()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetMouseButton(0) ||
                UnityEngine.Input.GetMouseButtonUp(0))
                return 1;
            return 0;
        }

        public override TouchInfo GetTouch(int touchIndex)
        {
            Vector2 position = UnityEngine.Input.mousePosition;
            Vector2 positionDelta = position - oldMousePosition;
            float timeDelta = Time.realtimeSinceStartup - deltaTime;
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                tapCount += 1;
                startActionTime = Time.realtimeSinceStartup;
                deltaTime = startActionTime;
                oldMousePosition = position;
                return new TouchInfo()
                {
                    FingerId = touchIndex,
                    TapCount = tapCount,
                    Position = position,
                    PositionDelta = Vector2.zero,
                    TimeDelta = 0,
                    Phase = TouchPhase.Began
                };
            }
            if (UnityEngine.Input.GetMouseButton(0))
            {
                var phase = positionDelta.sqrMagnitude < 1 ? TouchPhase.Stationary : TouchPhase.Moved;
                oldMousePosition = position;
                deltaTime = Time.realtimeSinceStartup;
                return new TouchInfo()
                {
                    FingerId = touchIndex,
                    TapCount = tapCount,
                    Position = position,
                    PositionDelta = positionDelta,
                    TimeDelta = timeDelta,
                    Phase = phase
                };
            }
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                oldMousePosition = position;
                return new TouchInfo()
                {
                    FingerId = touchIndex,
                    TapCount = tapCount,
                    Position = position,
                    PositionDelta = positionDelta,
                    TimeDelta = timeDelta,
                    Phase = TouchPhase.Ended
                };
            }
            {
                return new TouchInfo()
                {
                    FingerId = touchIndex,
                    TapCount = tapCount,
                    Position = position,
                    PositionDelta = positionDelta,
                    TimeDelta = timeDelta,
                    Phase = TouchPhase.Canceled
                };
            }
        }

        #endregion Public methods
    }
}
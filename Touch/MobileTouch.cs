using UnityEngine;

namespace BT.Input.Touch
{
    public class MobileTouch : MonoBehaviour, ISwipeRecognizer, ITapRecognizer, IPinchRecognizer, ITouchRecognizer
    {
        #region Events

        public event TouchEvent OnTap;
        public event TouchEvent OnTouchStart;
        public event TouchEvent OnTouch;
        public event TouchEvent OnTouchEnd;
        public event TouchEvent OnSwipeStart;
        public event TouchEvent OnSwipe;
        public event TouchEvent OnSwipeEnd;
        public event PinchEvent OnPinchStart;
        public event PinchEvent OnPinch;
        public event PinchEvent OnPinchEnd;

        #endregion Events

        #region Settings

        [SerializeField]
        private MobileTouchSettings settings;

        #endregion Settings

        #region Private members	

        private TouchInput input;
        private Touch currentTouch;
        private Pinch currentPinch;
        private float actionStartTime;

        #endregion Private members
        
        private void Awake()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            input = new MouseTouchInput();
#else
            input = new RealTouchInput();
#endif
            if (settings == null)
                throw new TouchException("Settings is NULL");
        }

        private void Reset()
        {
            settings = Resources.Load<MobileTouchSettings>(Constants.SETTINGS_FILE_NAME);
            if (settings == null)
                Debug.LogError("SettingsFile not found, fill it manually");
        }

        private void Update()
        {
            UpdateTouches();
        }

        private void UpdateTouches()
        {
            if (input == null || input.TouchCount() == 0)
                return;
            if (input.TouchCount() == 1)
            {
                TouchInfo touchInfo = input.GetTouch(0);
                if (currentTouch.GestureType == GestureType.None)
                    actionStartTime = Time.realtimeSinceStartup;
                touchInfo.StartTime = actionStartTime;
                if (touchInfo.Phase == TouchPhase.Began)
                {
                    Touch touch = new Touch
                    {
                        TouchInfo = touchInfo,
                        GestureType = GestureType.None,
                        SwipeDirection = SwipeDirection.None,
                        SwipeLength = 0,
                        SwipeVector = Vector2.zero
                    };
                    currentTouch = touch;
                    OnTouchStart?.Invoke(touch);
                }
                if (touchInfo.Phase == TouchPhase.Moved)
                {
                    OnTouch?.Invoke(new Touch
                    {
                        TouchInfo = touchInfo,
                        GestureType = GestureType.None,
                    });
                    if (touchInfo.PositionDelta.magnitude >= settings.GetSwipeLengthInPixels())
                    {
                        Touch touch = new Touch
                        {
                            TouchInfo = touchInfo,
                            GestureType = GestureType.Swipe,
                            SwipeDirection = GetSwipeDirection(touchInfo.Position - touchInfo.PositionDelta, touchInfo.Position),
                            SwipeLength = touchInfo.PositionDelta.magnitude,
                            SwipeVector = touchInfo.PositionDelta
                        };
                        switch (currentTouch.GestureType)
                        {
                            case GestureType.None:
                                OnSwipeStart?.Invoke(touch);
                                break;
                            case GestureType.Swipe:
                                OnSwipe?.Invoke(touch);
                                break;
                        }
                        currentTouch = touch;
                        return;
                    }
                }
                    
                if (touchInfo.Phase == TouchPhase.Stationary)
                {
                    OnTouch?.Invoke(new Touch
                    {
                        TouchInfo = touchInfo,
                        GestureType = GestureType.None,
                    });
                    touchInfo.ActionTime = Time.realtimeSinceStartup - touchInfo.StartTime;
                    currentTouch = new Touch
                    {
                        TouchInfo = touchInfo,
                        GestureType = GestureType.Tap,
                        SwipeDirection = SwipeDirection.None,
                        SwipeLength = 0,
                        SwipeVector = Vector2.zero
                    };
                }
                if (touchInfo.Phase == TouchPhase.Ended)
                {
                    OnTouchEnd?.Invoke(new Touch
                    {
                        TouchInfo = touchInfo,
                        GestureType = GestureType.None,
                    });
                    switch (currentTouch.GestureType)
                    {
                        case GestureType.Swipe:
                            OnSwipeEnd?.Invoke(currentTouch);
                            break;
                        case GestureType.Tap when currentTouch.TouchInfo.ActionTime <= settings.TapTime:
                            OnTap?.Invoke(currentTouch);
                            break;
                    }
                }
            }
            if (input.TouchCount() == 2)
            {
                TouchInfo touch0 = input.GetTouch(0);
                TouchInfo touch1 = input.GetTouch(1);
                if (currentTouch.GestureType == GestureType.None)
                    actionStartTime = Time.realtimeSinceStartup;
                touch0.StartTime = actionStartTime;
                if (touch0.Phase == TouchPhase.Began || touch1.Phase == TouchPhase.Began)
                {
                    Pinch pinch = new Pinch
                    {
                        TouchInfos = new [] {touch0, touch1},
                        GestureType = GestureType.None,
                        PinchVector = touch0.Position - touch1.Position
                    };
                    currentPinch = pinch;
                    OnPinchStart?.Invoke(currentPinch);
                }
                if (touch0.Phase != TouchPhase.Stationary 
                    || touch0.Phase == TouchPhase.Moved
                    || touch1.Phase != TouchPhase.Stationary 
                    || touch1.Phase == TouchPhase.Moved)
                {
                    Pinch pinch = new Pinch
                    {
                        TouchInfos = new [] {touch0, touch1},
                        GestureType = GestureType.Pinch,
                        PinchVector = touch0.Position - touch1.Position
                    };
                    currentPinch = pinch;
                    OnPinch?.Invoke(currentPinch);
                }
                if (touch0.Phase == TouchPhase.Ended || touch1.Phase == TouchPhase.Ended)
                {
                    Pinch pinch = new Pinch
                    {
                        TouchInfos = new [] {touch0, touch1},
                        GestureType = GestureType.Pinch,
                        PinchVector = touch0.Position - touch1.Position
                    };
                    currentPinch = pinch;
                    OnPinchEnd?.Invoke(currentPinch);
                }
            }
        }

        private SwipeDirection GetSwipeDirection(Vector2 start, Vector2 end)
        {
            Vector2 linear = (end - start).normalized;
            if (Vector2.Dot(linear, Vector2.up) >= settings.SwipeTolerance)
                return SwipeDirection.Up;
            if (Vector2.Dot(linear, Vector2.down) >= settings.SwipeTolerance)
                return SwipeDirection.Down;
            if (Vector2.Dot(linear, Vector2.right) >= settings.SwipeTolerance)
                return SwipeDirection.Right;
            if (Vector2.Dot(linear, Vector2.left) >= settings.SwipeTolerance)
                return SwipeDirection.Left;
            if (Vector2.Dot(linear, new Vector2(0.5f, 0.5f).normalized) >= settings.SwipeTolerance)
                return SwipeDirection.UpRight;
            if (Vector2.Dot(linear, new Vector2(0.5f, -0.5f).normalized) >= settings.SwipeTolerance)
                return SwipeDirection.DownRight;
            if (Vector2.Dot(linear, new Vector2(-0.5f, 0.5f).normalized) >= settings.SwipeTolerance)
                return SwipeDirection.UpLeft;
            if (Vector2.Dot(linear, new Vector2(-0.5f, -0.5f).normalized) >= settings.SwipeTolerance)
                return SwipeDirection.DownLeft;
            return SwipeDirection.Other;
        }
    }
}
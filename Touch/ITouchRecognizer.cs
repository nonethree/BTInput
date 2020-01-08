namespace BT.Input.Touch
{
    public interface ITouchRecognizer
    {
        event TouchEvent OnTouchStart;
        event TouchEvent OnTouch;
        event TouchEvent OnTouchEnd;
    }
}

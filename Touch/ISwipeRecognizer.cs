namespace BT.Input.Touch
{
    public interface ISwipeRecognizer
    {
        event TouchEvent OnSwipeStart;
        event TouchEvent OnSwipe;
        event TouchEvent OnSwipeEnd;
    }
}
namespace BT.Input.Touch
{
    public interface IPinchRecognizer
    {
        event PinchEvent OnPinchStart;
        event PinchEvent OnPinch;
        event PinchEvent OnPinchEnd;
    }
}
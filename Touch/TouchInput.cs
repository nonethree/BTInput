namespace BT.Input.Touch
{
    public abstract class TouchInput
    {
        public abstract int TouchCount();
        public abstract TouchInfo GetTouch(int touchIndex);
    }
}

using System;

namespace BT.Input.Touch
{
    public class TouchException : Exception
    {
        public TouchException(string message) : base(message) { }
    }
}
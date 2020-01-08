using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT.Input.Touch
{
    public interface ITapRecognizer
    {
         event TouchEvent OnTap;
    }
}

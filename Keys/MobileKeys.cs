using System;
using UnityEngine;

namespace BT.Input.Keys
{
    public class MobileKeys : MonoBehaviour, IBackInput
    {
        public event Action OnBack;

        private bool backPressed;

        private void Update()
        {
#if UNITY_ANDROID || UNITY_EDITOR
            if (UnityEngine.Input.GetKey(KeyCode.Escape) && !backPressed) 
                OnBack?.Invoke();
            if (UnityEngine.Input.GetKeyUp(KeyCode.Escape) && backPressed)
                backPressed = false;
#endif
        }
    }
}
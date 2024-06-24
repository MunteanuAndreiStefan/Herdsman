using UnityEngine;

namespace GameInput.Interfaces
{
    public interface IInputManager
    {
        public Vector3 GetInputPosition();
        public bool IsInputActive();
        bool GetMenuKey();
    }
}
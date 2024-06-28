using UnityEngine;

namespace GameInput.Interfaces
{
    /// <summary>
    /// InputManager interface
    /// </summary>
    public interface IInputManager
    {
        public Vector3 GetInputPosition();
        public bool IsInputActive();
        public bool GetMenuKey();
    }
}
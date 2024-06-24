using GameInput.Interfaces;
using UnityEngine;

namespace GameInput
{
    public class InputManager : IInputManager
    {
        private readonly AbstractInputHandler _abstractInputHandler;

        public InputManager(AbstractInputHandler abstractInputHandler) => _abstractInputHandler = abstractInputHandler;

        public Vector3 GetInputPosition() => _abstractInputHandler.GetInputPosition();

        public bool IsInputActive() => _abstractInputHandler.IsInputActive();

        public bool GetMenuKey() => _abstractInputHandler.GetMenuKey();
    }
}
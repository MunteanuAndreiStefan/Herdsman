using GameInput.Interfaces;
using UnityEngine;

namespace GameInput
{
    /// <summary>
    /// InputManager, it's used like a proxy pattern, in order to manage the input for the game.
    /// </summary>
    public class InputManager : IInputManager
    {
        private readonly AbstractInputHandler _abstractInputHandler;

        public InputManager(AbstractInputHandler abstractInputHandler) => _abstractInputHandler = abstractInputHandler;

        public Vector3 GetInputPosition() => _abstractInputHandler.GetInputPosition();

        public bool IsInputActive() => _abstractInputHandler.IsInputActive();

        public bool GetMenuKey() => _abstractInputHandler.GetMenuKey();
    }
}
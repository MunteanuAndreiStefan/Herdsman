using UnityEngine;

namespace GameInput.Interfaces
{
    /// <summary>
    /// AbstractInputHandler, used to define the input handler for the game.
    /// </summary>
    public abstract class AbstractInputHandler
    {
        public abstract Vector3 GetInputPosition();
        public virtual bool IsInputActive() =>  Time.timeScale > 0f;
        public abstract bool GetMenuKey();
    }
}
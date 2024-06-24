using UnityEngine;

namespace GameInput.Interfaces
{
    public abstract class AbstractInputHandler
    {
        public abstract Vector3 GetInputPosition();
        public virtual bool IsInputActive() =>  Time.timeScale > 0f;
        public abstract bool GetMenuKey();
    }
}
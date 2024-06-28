using GameCamera;
using GameInput.Interfaces;
using UnityEngine;

namespace GameInput
{
    /// <summary>
    /// TouchInput handler is handles the touch input for the game.
    /// </summary>
    public class TouchInputHandler : AbstractInputHandler
    {
        /// <summary>
        /// Get the input position from the touch.
        /// </summary>
        /// <returns>Vector3 position in the world based on screen input.</returns>
        public override Vector3 GetInputPosition() =>
            Input.touchCount > 0
                ? CameraManager.GetCurrent.ScreenToWorldPoint(Input.GetTouch(0).position)
                : Vector3.zero;

        /// <summary>
        /// Checks if touch is on the screen.
        /// </summary>
        /// <returns>Returns true if user pressed something on the screen</returns>
        public override bool IsInputActive() => base.IsInputActive() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

        /// <summary>
        /// Checks if user pressed the menu key.
        /// </summary>
        /// <returns>Returns true if user pressed something for the menu</returns>
        public override bool GetMenuKey()
        {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
            return true;
#elif UNITY_IOS
        if (Input.GetKeyDown(KeyCode.Home))
            return true;
#endif
            return false;
        }
    }
}
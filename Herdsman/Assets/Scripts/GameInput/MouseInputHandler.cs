using GameCamera;
using GameInput.Interfaces;
using UnityEngine;

namespace GameInput
{
    /// <summary>
    /// MouseInputHandler is the handles the mouse input for the game.
    /// </summary>
    public class MouseInputHandler : AbstractInputHandler
    {
        public override Vector3 GetInputPosition() => CameraManager.GetCurrent.ScreenToWorldPoint(Input.mousePosition);

        public override bool GetMenuKey() => Input.GetKeyDown(KeyCode.Escape);

        public override bool IsInputActive() => base.IsInputActive() && Input.GetMouseButtonDown(0);
    }
}
using GameCamera;
using GameInput.Interfaces;
using UnityEngine;

namespace GameInput
{
    public class TouchInputHandler : AbstractInputHandler
    {
        public override Vector3 GetInputPosition() =>
            Input.touchCount > 0
                ? CameraManager.GetCurrent.ScreenToWorldPoint(Input.GetTouch(0).position)
                : Vector3.zero;

        public override bool IsInputActive() => base.IsInputActive() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

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
using UnityEngine;
using Utils.Generics;

namespace GameCamera
{
    public class CameraManager : PersistentSingleton<CameraManager>
    {
        private const string MAIN_CAMERA_PATH = "MainCamera";
        private Camera MainCamera { get; set; }
        public static Camera GetCurrent => Instance.MainCamera; // TODO implement support for multiple cameras.

        public override void Awake()
        {
            base.Awake();
            if (Camera.main == null) //Camera.main can be become, so a management system for cameras is needed.
            {
                Debug.LogError("Main camera not found in the scene");
                Instantiate(Resources.Load("Camera/MAIN_CAMERA_PATH"), Vector3.zero, Quaternion.identity);
            }

            MainCamera = Camera.main;
        }
    }
}

using UnityEngine;
using Utils;
using Utils.Generics;

namespace GameCamera
{
    /// <summary>
    /// Camera manager obtain current camera in the scene.
    /// </summary>
    public class CameraManager : PersistentSingleton<CameraManager>
    {
        private const string MAIN_CAMERA_PATH = "Camera/MainCamera";
        private Camera MainCamera { get; set; }
        public static Camera GetCurrent => Instance.MainCamera; // TODO implement support for multiple cameras.

        private async void OnEnable()
        {
            if (Camera.main == null) //Camera.main can be become, so a management system for cameras is needed.
            {
                Debug.Log("Main camera not found in the scene, it was added async.");
                var assetProvider = await AssetProvider.InstanceAsync();
                var mainCameraPrefab = await assetProvider.LoadAssetAsync<GameObject>(MAIN_CAMERA_PATH);
                if (mainCameraPrefab != null) 
                    Instantiate(mainCameraPrefab);
            }

            MainCamera = Camera.main;
            AssetProvider.Instance.ReleaseAsset(MAIN_CAMERA_PATH);
        }
    }
}
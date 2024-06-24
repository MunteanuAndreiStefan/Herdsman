using GameUI.Interfaces;
using UnityEngine;
using Utils.Generics;

namespace GameUI
{
    public class UIManager : PersistentSingleton<UIManager>, IUIManager
    {
        [SerializeField] private GameObject _staticCanvas;
        [SerializeField] private GameObject _dynamicCanvas;
        private bool _isPaused;
        private StaticMenu _staticMenu;

        public void SetupCanvas(GameObject staticCanvas, GameObject dynamicCanvas)
        {
            _staticCanvas = staticCanvas;
            _dynamicCanvas = dynamicCanvas;
            _staticMenu = _staticCanvas.GetComponent<StaticMenu>();
        }

        private void Start()
        {
            if (_staticCanvas == null)
                Debug.LogError("Static Canvas is not assigned in the inspector.", this);

            if (_dynamicCanvas == null)
                Debug.LogError("Dynamic Canvas is not assigned in the inspector.", this);
        }

        public void StartGame()
        {
            ActivateStaticCanvas();
            UnpauseGame();
        }

        public void SaveGame() => Debug.Log("Game Saved!");

        public void LoadGame() => throw new System.NotImplementedException();

        public void ActivateStaticCanvas()
        {
            if (_staticCanvas == null) return;

            _staticCanvas.SetActive(true);
        }

        public void DeactivateStaticCanvas()
        {
            if (_staticCanvas == null) return;

            _staticCanvas.SetActive(false);
        }

        public void ActivateDynamicCanvas()
        {
            if (_dynamicCanvas == null) return;

            _dynamicCanvas.SetActive(true);
        }

        public void DeactivateDynamicCanvas()
        {
            if (_dynamicCanvas == null) return;

            _dynamicCanvas.SetActive(false);
        }

        private void UnpauseGame()
        {
            _isPaused = false;
            Time.timeScale = 1f;
            _staticMenu.SetMenuState(false);
        }

        public void PauseGame()
        {
            _staticMenu.SetMenuState(!_isPaused);
            _isPaused = !_isPaused;

            Time.timeScale = _isPaused ? 0f : 1f;
        }

        public void ExitGame() => Application.Quit();
    }
}
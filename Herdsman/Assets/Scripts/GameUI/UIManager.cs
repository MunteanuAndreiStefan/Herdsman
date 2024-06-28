using GameUI.Interfaces;
using UnityEngine;
using Utils.Generics;

namespace GameUI
{
    /// <summary>
    /// UIManager, handles all UI related operations.
    /// </summary>
    public class UIManager : PersistentSingleton<UIManager>, IUIManager
    {
        [SerializeField] private GameObject _staticCanvas;
        [SerializeField] private GameObject _dynamicCanvas;
        private bool _isPaused;
        private StaticMenu _staticMenu;

        /// <summary>
        /// Setups the static and dynamic canvas in order to optimize the performance, by not setting the canvas as dirty.
        /// </summary>
        public void SetupCanvas(GameObject staticCanvas, GameObject dynamicCanvas)
        {
            _staticCanvas = staticCanvas;
            _dynamicCanvas = dynamicCanvas;
            _staticMenu = _staticCanvas.GetComponent<StaticMenu>();
        }

        /// <summary>
        /// Start game by activating the static canvas and unpausing the game.
        /// </summary>
        public void StartGame()
        {
            ActivateStaticCanvas();
            UnpauseGame();
        }

        /// <summary>
        /// Save game, currently not implmented. #TODO: Implement save game feature.
        /// </summary>
        public void SaveGame() => Debug.Log("Game Saved!");

        /// <summary>
        /// Load game, currently not implmented. #TODO: Implement load game feature.
        /// </summary>
        public void LoadGame() => throw new System.NotImplementedException();

        /// <summary>
        /// Activates the static canvas. - Some extra functionality may be added in the future.
        /// Like register and unregister events or panels based on activation and deactivation, no need to have one function for both.
        /// </summary>
        public void ActivateStaticCanvas() => SetCanvasState(_staticCanvas, true);

        /// <summary>
        /// Deactivates the static canvas.
        /// </summary>
        public void DeactivateStaticCanvas() => SetCanvasState(_staticCanvas, false);

        /// <summary>
        /// Activates the dynamic canvas.
        /// </summary>
        public void ActivateDynamicCanvas() => SetCanvasState(_dynamicCanvas, true);

        /// <summary>
        /// Deactivate the dynamic canvas.
        /// </summary>
        public void DeactivateDynamicCanvas() => SetCanvasState(_dynamicCanvas, false);

        /// <summary>
        /// Exits the game.
        /// </summary>
        public void ExitGame() => Application.Quit();
        
        private void SetCanvasState(GameObject canvas, bool state)
        {
            if (canvas == null) return;

            canvas.SetActive(state);
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
        
        private void Start()
        {
            if (_staticCanvas == null)
                Debug.LogError("Static Canvas is not assigned in the inspector.", this);

            if (_dynamicCanvas == null)
                Debug.LogError("Dynamic Canvas is not assigned in the inspector.", this);
        }
    }
}
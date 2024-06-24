using GameCore;
using Microsoft.Extensions.DependencyInjection;
using GameUI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class StaticMenu : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private GameObject _menuPanel;
        private IUIManager _uiManager;

        public void SetMenuState(bool state) => _menuPanel.SetActive(state);

        private void Start()
        {
            _uiManager = DiContainer.Instance.ServiceProvider.GetRequiredService<IUIManager>();
            _resumeButton.onClick.AddListener(_uiManager.StartGame);
            _exitButton.onClick.AddListener(_uiManager.ExitGame);
        }
    }
}
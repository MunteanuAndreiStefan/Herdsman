using GameCore;
using GameUI.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TMPro;
using UnityEngine;
using UniRx;

namespace GameUI
{
    /// <summary>
    /// This could be done as well with observer patterns, but UniRx is a powerful library, and doesn't make any sense to register/unregister something that will always be used in the project.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UIScoreObserver : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _scoreText; // Better quality, performance and features than Text component

        private void Awake()
        {
            if (_scoreText == null)
                _scoreText = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            var scoreManager = DiContainer.Instance.ServiceProvider.GetRequiredService<IScoreManager>();

            scoreManager.Score
                .Subscribe(UpdateScoreUI)
                .AddTo(this); // Ensures subscription is disposed of when the GameObject is destroyed
        }

        private void OnValidate()
        {
            if (_scoreText == null)
                Debug.LogWarning("Score Text is not assigned in the inspector.", this);
        }

        private void UpdateScoreUI(int newScore) =>
            _scoreText.text = newScore.ToString(); // There is no need of adding "Score: " + newValue each time, this ensures non-alloc memory, better even than string interpolation or string builder, at the same time having 2 canvases ensure the canvas will not get marked as dirty each time the score changes.
    }
}
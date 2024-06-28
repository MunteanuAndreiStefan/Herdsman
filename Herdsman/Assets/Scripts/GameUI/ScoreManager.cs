using GameUI.Interfaces;
using UniRx;

namespace GameUI
{
    /// <summary>
    /// ScoreManager handles the score of the game
    /// </summary>
    public class ScoreManager : IScoreManager
    {
        private readonly ReactiveProperty<int> _score = new ReactiveProperty<int>(0);

        /// <summary>
        /// IReactive property for the score, used to observe the score changes.
        /// </summary>
        public IReadOnlyReactiveProperty<int> Score => _score;

        /// <summary>
        /// Increase score by 1.
        /// </summary>
        public void IncreaseScore() => _score.Value++;

        public int GetScore() => _score.Value;
    }
}
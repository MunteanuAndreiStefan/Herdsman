using GameUI.Interfaces;
using UniRx;

namespace GameUI
{
    public class ScoreManager : IScoreManager
    {
        private readonly ReactiveProperty<int> _score = new ReactiveProperty<int>(0);

        public IReadOnlyReactiveProperty<int> Score => _score;

        public void IncreaseScore() => _score.Value++;

        public int GetScore() => _score.Value;
    }
}
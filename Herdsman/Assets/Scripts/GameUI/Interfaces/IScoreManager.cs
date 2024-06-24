using UniRx;

namespace GameUI.Interfaces
{
    public interface IScoreManager
    {
        IReadOnlyReactiveProperty<int> Score { get; }
        void IncreaseScore();
    }
}
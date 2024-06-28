using UniRx;

namespace GameUI.Interfaces
{
    /// <summary>
    /// IScoreManager interface
    /// </summary>
    public interface IScoreManager
    {
        IReadOnlyReactiveProperty<int> Score { get; }
        void IncreaseScore();
    }
}
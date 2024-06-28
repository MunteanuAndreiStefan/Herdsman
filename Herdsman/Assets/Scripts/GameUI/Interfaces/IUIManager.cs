using UnityEngine;

namespace GameUI.Interfaces
{
    /// <summary>
    /// IUIManager interface, composed of multiple UI interfaces
    /// </summary>
    public interface IUIManager : IGameState, IGameSave, IStaticCanvas, IDynamicCanvas, IGameExit // Interface segregation principle
    {
        public void SetupCanvas(GameObject staticCanvas, GameObject dynamicCanvas);
    }
}
using UnityEngine;

namespace GameUI.Interfaces
{
    public interface IUIManager : IGameState, IGameSave, IStaticCanvas, IDynamicCanvas, IGameExit // Interface segregation principle
    {
        void SetupCanvas(GameObject staticCanvas, GameObject dynamicCanvas);
    }
}
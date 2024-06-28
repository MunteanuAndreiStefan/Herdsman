using UnityEngine;

namespace GameCore.Animal.Interfaces
{
    /// <summary>
    /// AnimalBehavior interface
    /// </summary>
    public interface IAnimalBehavior
    {
        public abstract void SetTarget(Transform newTarget, IAnimalObserver observer);
        public void UpdateBehavior();
    }

}
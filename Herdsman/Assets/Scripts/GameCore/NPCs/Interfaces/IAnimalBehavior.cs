using UnityEngine;

namespace GameCore.Animal.Interfaces
{
    public interface IAnimalBehavior
    {
        public abstract void SetTarget(Transform newTarget, IAnimalObserver observer);
        public void UpdateBehavior();
    }

}
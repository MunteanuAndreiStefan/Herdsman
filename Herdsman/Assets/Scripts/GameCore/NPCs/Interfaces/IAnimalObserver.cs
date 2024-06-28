using UnityEngine;

namespace GameCore.Animal.Interfaces
{
    /// <summary>
    /// AnimalObserver interface
    /// </summary>
    public interface IAnimalObserver
    {
        public void OnAnimalDisabled(Collider2D collider2d);
    }
}
using UnityEngine;

namespace GameCore.Animal.Interfaces
{
    public interface IAnimalObserver
    {
        void OnAnimalDisabled(Collider2D collider2d);
    }
}
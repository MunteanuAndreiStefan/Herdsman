using UnityEngine;

namespace GameCore.Animal.Interfaces
{
    public interface IAnimalObserver
    {
        public void OnAnimalDisabled(Collider2D collider2d);
    }
}
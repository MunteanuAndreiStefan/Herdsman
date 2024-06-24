using UnityEngine;

namespace GameCore.Animal
{
    public interface IAnimal
    {
        void Spawn(Vector3 position);
        void Deactivate();
    }
}
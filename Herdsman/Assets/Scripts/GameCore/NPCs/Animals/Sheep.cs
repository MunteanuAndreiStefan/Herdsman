using UnityEngine;

namespace GameCore.Animal
{
    public class Sheep : MonoBehaviour, IAnimal
    {
        public void Spawn(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public void Deactivate() => gameObject.SetActive(false);
    }
}
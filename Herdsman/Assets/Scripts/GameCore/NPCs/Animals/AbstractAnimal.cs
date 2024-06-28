using UnityEngine;

namespace GameCore.Animal
{
    /// <summary>
    /// Base class for all animals, contains basic logic for spawning and deactivating and rendering.
    /// </summary>
    public class AbstractAnimal : MonoBehaviour, IAnimal
    {
        /// <summary>
        /// Event that is triggered when the animal is destroyed.
        /// </summary>
        public event IAnimal.DestroyedAction OnDestroyed;
        [SerializeField] private Material _material;
        [SerializeField] private Mesh _mesh;
        
        /// <summary>
        /// Mesh of the animal, by default a quad.
        /// </summary>
        public Mesh GetMesh() => _mesh;

        /// <summary>
        /// Material of the animal.
        /// </summary>
        public Material GetMaterial() => _material;

        /// <summary>
        /// Deactivates the animal.
        /// </summary>
        public void Deactivate()
        {
            OnDestroyed?.Invoke();
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Spawns the animal at the given position.
        /// </summary>
        /// <param name="position">Position of spawn</param>
        public void Spawn(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Gets the current matrix of the animal, to be used for rendering.
        /// </summary>
        /// <returns>Matrix4x4 for current animal.</returns>
        public Matrix4x4 GetCurrentMatrix()
        {
            var position = transform.position;
            var rotation = Quaternion.identity;
            var scale = new Vector3( DiContainer.Instance.GameConfig.AnimalSize, DiContainer.Instance.GameConfig.AnimalSize, DiContainer.Instance.GameConfig.AnimalSize);
            return Matrix4x4.TRS(position, rotation, scale);
        }
    }
}
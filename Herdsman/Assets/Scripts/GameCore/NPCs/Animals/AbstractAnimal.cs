using UnityEngine;
using Utils;

namespace GameCore.Animal
{
    public class AbstractAnimal : MonoBehaviour, IAnimal
    {
        public event IAnimal.DestroyedAction OnDestroyed;
        [SerializeField] private Material _material;
        [SerializeField] private Mesh _mesh;
        
        public Mesh GetMesh() => _mesh;
        public Material GetMaterial() => _material;

        public void Deactivate()
        {
            OnDestroyed?.Invoke();
            gameObject.SetActive(false);
        }

        public void Spawn(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public Matrix4x4 GetCurrentMatrix()
        {
            var position = transform.position;
            var rotation = Quaternion.identity;
            var scale = new Vector3(Constants.DEFAULT_SIZE_ANIMAL, Constants.DEFAULT_SIZE_ANIMAL, Constants.DEFAULT_SIZE_ANIMAL);
            return Matrix4x4.TRS(position, rotation, scale);
        }
    }
}
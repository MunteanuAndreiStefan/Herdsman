using UnityEngine;

namespace GameCore.Animal
{
    /// <summary>
    /// Animal interface
    /// </summary>
    public interface IAnimal
    {
        public Mesh GetMesh();
        public Material GetMaterial();
        public Matrix4x4 GetCurrentMatrix();
        public void Spawn(Vector3 position);
        public void Deactivate();
        public delegate void DestroyedAction();
        public event DestroyedAction OnDestroyed;
    }
}
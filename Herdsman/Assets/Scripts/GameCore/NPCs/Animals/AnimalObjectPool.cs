using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Animal
{
    /// <summary>
    /// Generic object pool for animals
    /// </summary>
    public class AnimalObjectPool<T> where T : MonoBehaviour, IAnimal
    {
        private readonly Queue<T> _pool;
        private readonly T _prefab;

        /// <summary>
        /// Constructor for the pool
        /// </summary>
        /// <param name="prefab">Prefab used on the pool</param>
        /// <param name="initialCapacity">Initial capacity of the pool</param>
        public AnimalObjectPool(T prefab, int initialCapacity = 10)
        {
            _prefab = prefab;
            _pool = new Queue<T>();

            for (var i = 0; i < initialCapacity; i++)
            {
                var obj = Object.Instantiate(prefab);
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
        }

        /// <summary>
        /// Get an object from the pool
        /// </summary>
        public T Get()
        {
            if (_pool.Count > 0)
            {
                var obj = _pool.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var obj = Object.Instantiate(_prefab);
                return obj;
            }
        }

        /// <summary>
        /// Return an object to the pool
        /// </summary>
        public void ReturnToPool(T obj)
        {
            obj.Deactivate();
            _pool.Enqueue(obj);
        }
    }
}
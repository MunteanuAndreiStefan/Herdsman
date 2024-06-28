using System.Threading.Tasks;
using UnityEngine;

namespace Utils.Generics
{
    /// <summary> 
    /// Persistent generic singleton for components.
    /// </summary>
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        private static readonly TaskCompletionSource<T> _instanceTokenComplete = new TaskCompletionSource<T>();
        
        public static async Task<T> InstanceAsync()
        {
            if (_instance != null) return _instance;

            _instance = FindFirstObjectByType<T>();
            if (_instance != null)
            {
                _instanceTokenComplete.SetResult(_instance);
                return _instance;
            }

            var prefab = await LoadPrefabAsync(Constants.EMPTY_PREFAB);
            var obj = Instantiate(prefab);
            obj.name = typeof(T).Name;
            _instance = obj.AddComponent<T>();
            DontDestroyOnLoad(obj);
            _instanceTokenComplete.SetResult(_instance);
            return _instance;
        }

        private static async Task<GameObject> LoadPrefabAsync(string path)
        {
            var resourceRequest = Resources.LoadAsync<GameObject>(path);
            await resourceRequest;
            return resourceRequest.asset as GameObject;
        }

        public static T Instance
        { 
            get
            {
                if (_instance != null) return _instance;

                _instance = FindFirstObjectByType<T>();
                if (_instance != null) return _instance;

                var prefab = Resources.Load<GameObject>(Constants.EMPTY_PREFAB);
                var obj = Instantiate(prefab);
                obj.name = typeof(T).Name;
                _instance = obj.AddComponent<T>();
                DontDestroyOnLoad(obj);
                return _instance;
            }
        }

        public virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (this != _instance)
                    Destroy(gameObject);
            }
        }
    }
}
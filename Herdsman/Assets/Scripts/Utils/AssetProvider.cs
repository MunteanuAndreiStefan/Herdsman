using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utils.Generics;

namespace Utils
{
    /// <summary>
    /// AssetProvider is a utility class for loading async any prefab.
    /// </summary>
    public class AssetProvider : PersistentSingleton<AssetProvider>
    {
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        /// <summary>
        /// Loads an asset async.
        /// </summary>
        /// <param name="address">Path to the address</param>
        /// <typeparam name="T">Asset type</typeparam>
        /// <returns>Task with a result, null if failed.</returns>
        public async Task<T> LoadAssetAsync<T>(string address) where T : Object
        {
            if (_cache.TryGetValue(address, out var value))
                return (T)value;

            var handle = Addressables.LoadAssetAsync<T>(address);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _cache[address] = handle.Result;
                return handle.Result;
            }

            Debug.LogError($"Failed to load asset at address: {address}");
            return null;
        }

        /// <summary>
        /// Release an asset from the cache.
        /// </summary>
        /// <param name="address">Path of the asset</param>
        public void ReleaseAsset(string address)
        {
            if (!_cache.TryGetValue(address, out var value)) return;

            Addressables.Release(value);
            _cache.Remove(address);
        }

        /// <summary>
        /// Clean all the asset cache.
        /// </summary>
        public void ClearCache()
        {
            foreach (var kvp in _cache)
                Addressables.Release(kvp.Value);
            _cache.Clear();
        }
    }
}
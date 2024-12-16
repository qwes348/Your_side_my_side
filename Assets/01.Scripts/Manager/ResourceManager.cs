using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

public class ResourceManager
{
    public async UniTask<T> LoadAsset<T>(string key)
    {
        var asset = await Addressables.LoadAssetAsync<T>(key);
        return asset;
    }

    public async UniTask<T> LoadAsset<T>(AssetReference reference)
    {
        var asset = await Addressables.LoadAssetAsync<T>(reference);
        return asset;
    }

    public async UniTask<T> InstantiateAsset<T>(string key, Transform parent = null)
    {
        var go = await Addressables.InstantiateAsync(key, parent);
        return go.GetComponent<T>();
    }

    public async UniTask<T> InstantiateAsset<T>(AssetReference reference, Transform parent = null)
    {
        var go = await Addressables.InstantiateAsync(reference.RuntimeKey, parent);
        return go.GetComponent<T>();
    }

    public async UniTask<List<T>> LoadAssetsByLabel<T>(string label)
    {
        var assets = await Addressables.LoadAssetsAsync<T>(label, null);
        return assets.ToList();
    }
}

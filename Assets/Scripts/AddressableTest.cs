using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

public class AddressableTest : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject toLoad;
    [SerializeField] private List<AssetReferenceGameObject> othersToLoad;

    [SerializeField] private GameObject parentToBe;

    void Awake()
    {
        if (parentToBe != null)
            parentToBe = this.gameObject;
    }

    private void Start()
    {
        // only for debugging
        Caching.ClearCache();

        Debug.Log($"patt :: AddressableTest :: Start");

        Addressables.LoadAssetAsync<GameObject>(toLoad).Completed += OnItemLoadComplete;

        foreach(var item in othersToLoad)
            Addressables.LoadAssetAsync<GameObject>(item).Completed += OnItemLoadComplete;
    }

    private void OnItemLoadComplete(AsyncOperationHandle<GameObject> handle)
    {
        // Debug.Log($"patt :: download complete :: handle.Status = {handle.Status}");

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"patt :: Failed to gameObject :: {handle.OperationException.Message}");
            return;
        }

        // instantiate
        var created = Instantiate(handle.Result, parentToBe.transform);
        Debug.Log($"patt :: Created = {created}");
    }
}
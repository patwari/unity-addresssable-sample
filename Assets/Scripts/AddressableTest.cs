using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using UnityEngine.UI;

public class AddressableTest : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject sphereRef;
    [SerializeField] private AssetReferenceGameObject cubeRef;

    [SerializeField] private Button sphereButton;
    [SerializeField] private Button cubeButton;

    // [SerializeField] private List<AssetReferenceGameObject> additionalToLoad;

    [SerializeField] private GameObject parentToBe;

    private DOWNLOAD_STATUS sphereStatus = DOWNLOAD_STATUS.NOT_STARTED;
    private DOWNLOAD_STATUS cubeStatus = DOWNLOAD_STATUS.NOT_STARTED;

    void Awake()
    {
        if (parentToBe != null)
            parentToBe = this.gameObject;

        sphereButton.onClick.AddListener(DownloadSphere);
        cubeButton.onClick.AddListener(DownloadCube);
    }

    private void DownloadSphere()
    {
        if (sphereStatus == DOWNLOAD_STATUS.NOT_STARTED)
        {
            sphereStatus = DOWNLOAD_STATUS.DOWNLOADING;
            Addressables.LoadAssetAsync<GameObject>(sphereRef).Completed += (handle) =>
            {
                bool success = InstantiateOnItemLoadComplete(handle);
                sphereStatus = success ? DOWNLOAD_STATUS.DOWNLOADED : DOWNLOAD_STATUS.NOT_STARTED;
            };
        }
    }

    private void DownloadCube()
    {
        if (cubeStatus == DOWNLOAD_STATUS.NOT_STARTED)
        {
            cubeStatus = DOWNLOAD_STATUS.DOWNLOADING;
            Addressables.LoadAssetAsync<GameObject>(cubeRef).Completed += (handle) =>
            {
                bool success = InstantiateOnItemLoadComplete(handle);
                cubeStatus = success ? DOWNLOAD_STATUS.DOWNLOADED : DOWNLOAD_STATUS.NOT_STARTED;
            };
        }
    }

    private void Start()
    {
        // only for debugging
        Caching.ClearCache();
    }

    private bool InstantiateOnItemLoadComplete(AsyncOperationHandle<GameObject> handle)
    {
        Debug.Log($"patt :: download complete :: handle.Status = {handle.Status}");

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"patt :: Failed to gameObject :: {handle.OperationException.Message}");
            return false;
        }

        // instantiate
        var created = Instantiate(handle.Result, parentToBe.transform);
        Debug.Log($"patt :: Created = {created}");
        return true;
    }

    private enum DOWNLOAD_STATUS
    {
        NOT_STARTED,
        DOWNLOADING,
        DOWNLOADED
    }
}
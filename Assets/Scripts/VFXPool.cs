using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;
using UnityEngine.ResourceManagement.AsyncOperations;

public class VFXPool : MonoBehaviour
{
    public bool IsReady { get; private set; }
    public string ExplosionName = "";

    [SerializeField] private AssetReference vfxAssetRef;

    private GameObject loadedGameObject;
    private ObjectPool<GameObject> vfxPool;
    private AsyncOperationHandle<GameObject> loadHandle;

    IEnumerator Start()
    {
        IsReady = false;
        loadHandle = vfxAssetRef.LoadAssetAsync<GameObject>();
        yield return loadHandle;

        if (loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            CreateObjectPool(loadHandle.Result);
            IsReady = true;
        }
    }

    private void CreateObjectPool(GameObject loadedGameObject)
    {
        this.loadedGameObject = loadedGameObject;
        this.vfxPool = new ObjectPool<GameObject>(
            CreateInstance,
            obj => obj.SetActive(true),
            obj => obj.SetActive(false),
            obj => Destroy(obj), false, 20, 100);
    }

    public GameObject GetVFX()
    {
        return vfxPool.Get();
    }

    private GameObject CreateInstance()
    {
        return Instantiate(loadedGameObject, this.transform.position, this.transform.rotation, this.transform);
    }

    public void Return(GameObject gameObject)
    {
        vfxPool.Release(gameObject);
    }

    private void OnDestroy()
    {
        vfxPool.Dispose();
        Addressables.Release(loadHandle);
    }
}

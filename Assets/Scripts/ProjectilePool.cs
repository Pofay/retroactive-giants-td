using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ProjectilePool : MonoBehaviour
{
    public bool IsReady { get; private set; }

    [SerializeField] private AssetReference projectileAssetRef;

    private GameObject loadedProjectileObject;
    private ObjectPool<GameObject> projectilePool;
    private AsyncOperationHandle<GameObject> loadHandle;

    IEnumerator Start()
    {
        IsReady = false;
        loadHandle = projectileAssetRef.LoadAssetAsync<GameObject>();
        if (!loadHandle.IsDone)
        {
            yield return loadHandle;
        }

        if (loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            CreateObjectPool(loadHandle.Result);
            IsReady = true;
        }
    }

    private void CreateObjectPool(GameObject loadedGameObject)
    {
        this.loadedProjectileObject = loadedGameObject;
        this.projectilePool = new ObjectPool<GameObject>(
            CreateInstance,
            obj => obj.SetActive(true),
            obj => obj.SetActive(false),
            obj => Destroy(obj), false, 3, 5);

    }

    public GameObject GetProjectile()
    {
        return projectilePool.Get();
    }

    private GameObject CreateInstance()
    {
        return Instantiate(loadedProjectileObject, this.transform.position, this.transform.rotation, this.transform);
    }

    public void Return(Bullet bullet)
    {
        var bulletGO = bullet.gameObject;
        projectilePool.Release(bulletGO);
    }
}

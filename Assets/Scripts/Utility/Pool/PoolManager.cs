using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PoolManager : MonoBehaviour,IPoolManager {

    private Transform GameObjectPoolParent = null;
    private Dictionary<string, IObjectPool> objectPoolDic = new Dictionary<string, IObjectPool>();
    private Dictionary<string, IGameObjectPool> gameObjectPoolDic = new Dictionary<string, IGameObjectPool>();
    private static PoolManager instance;
    public static IPoolManager GetInstance()
    {
        if (instance != null)
            return instance;
        throw new Exception("PoolManager 没初始化呢");
    }
    private Transform PoolRootObject
    {
        get {
            if (GameObjectPoolParent == null)
            {
                var objectPool = new GameObject("Object Pool");
                objectPool.transform.SetParent(transform);
                objectPool.transform.localScale = Vector3.one;
                objectPool.transform.localPosition = Vector3.zero;
                GameObjectPoolParent = objectPool.transform;
                GameObjectPoolParent.gameObject.SetActive(false);
            }
            return GameObjectPoolParent;
        }
    }
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    public IObjectPoolManager ObjectPool
    {
        get {
            return this;
         }
    }

    public IGameObjectPoolManager GameObjectPool
    {
        get {
            return this;
        }
    }

    public GameObject GetGameObject(string poolName)
    {
        IGameObjectPool pool = GameObjectPool.GetGameObjectPool(poolName);
        if (pool != null)
        {
           return  pool.Get();
        }
        return null;
    }

    public T GetObject<T>() where T : class
    {
        IObjectPool pool = ObjectPool.GetObjectPool<T>();
        if (pool != null)
        {
            return  pool.Get<T>();
        }
        return null;
    }
    public object GetObject(string poolName)
    {
        if (objectPoolDic.ContainsKey(poolName))
        {
             return objectPoolDic[poolName].Get<object>();
        }
        return null;
    }

    public void ClearAll()
    {
        string[] keys = new string[gameObjectPoolDic.Count];
        gameObjectPoolDic.Keys.CopyTo(keys, 0);
        for (int i = 0; i < keys.Length; i++)
        {
            gameObjectPoolDic[keys[i]].ClearAll();
        }

        keys = new string[objectPoolDic.Count];
        objectPoolDic.Keys.CopyTo(keys, 0);
        for (int i = 0; i < keys.Length; i++)
        {
            objectPoolDic[keys[i]].ClearAll();
        }
    }

    public void ClearAll(string poolName)
    {
        if (gameObjectPoolDic.ContainsKey(poolName))
        {
            gameObjectPoolDic[poolName].ClearAll();
        }
        else if (objectPoolDic.ContainsKey(poolName))
        {
            objectPoolDic[poolName].ClearAll();
        }
    }

    public void ClearAll<T>() where T : class
    {
        string key = typeof(T).ToString();
        if (objectPoolDic.ContainsKey(key))
        {
            objectPoolDic[key].ClearAll();
        }
    }

    public void RemovePool(string poolName)
    {
        if (gameObjectPoolDic.ContainsKey(poolName))
        {
            gameObjectPoolDic[poolName].ClearAll();
            gameObjectPoolDic.Remove(poolName);
        }
        else if (objectPoolDic.ContainsKey(poolName))
        {
            objectPoolDic[poolName].ClearAll();
            objectPoolDic.Remove(poolName);
        }
    }

    public void RemovePool<T>() where T : class
    {
        string key = typeof(T).ToString();
        if (objectPoolDic.ContainsKey(key))
        {
            objectPoolDic[key].ClearAll();
            objectPoolDic.Remove(key);
        }
    }

    public IGameObjectPool CreateGameObjectPool(string poolName, GameObject prefab, int initSize = 3, int maxSize = 10)
    {
        if (prefab == null)
        {
            throw new System.Exception("basePrefab 参数不能为 null");
        }
        if (gameObjectPoolDic.ContainsKey(poolName))
        {
            Debug.LogWarning("[PoolManager.CreateGameObjectPool] " + poolName + " Pool Created Fail (Repeated)");
            return gameObjectPoolDic[poolName];
        }
        gameObjectPoolDic.Add(poolName, new GameObjectPool(prefab, PoolRootObject, initSize, maxSize));
        return gameObjectPoolDic[poolName];
    }

    public IGameObjectPool GetGameObjectPool(string poolName)
    {
        if (objectPoolDic.ContainsKey(poolName))
        {
            return gameObjectPoolDic[poolName];
        }
        Debug.LogError("[PoolManager.GetGameObjectPool] <" + poolName + "> Pool Not Create");
        return null;
    }

    public IObjectPool CreateObjectPool<T>(int initSize = 3, int maxSize = 10) where T : class
    {
        string key = typeof(T).ToString();
        if (objectPoolDic.ContainsKey(key))
        {
            return objectPoolDic[key];
        }
        objectPoolDic.Add(key, new ObjectPool<T>(initSize, maxSize));
        return objectPoolDic[key];
    }

    public IObjectPool CreateObjectPool<T>(int initSize = 3, int maxSize = 10, params object[] args) where T : class
    {
        string key = typeof(T).ToString();
        if (objectPoolDic.ContainsKey(key))
        {
            Debug.LogWarning("[PoolManager.CreateObjectPool] <" + key + "> Pool Created Fail (Repeated)");
            return objectPoolDic[key];
        }
        objectPoolDic.Add(key, new ObjectPool<T>(initSize, maxSize, args));
        return objectPoolDic[key];
    }

    public IObjectPool GetObjectPool<T>() where T : class
    {
        string key = typeof(T).ToString();
        if (objectPoolDic.ContainsKey(key))
        {
            return objectPoolDic[key];
        }
        return ObjectPool.CreateObjectPool<T>(3,10);
    }

    public IObjectPool GetObjectPool<T>(params object[] args) where T : class
    {
        string key = typeof(T).ToString();
        if (objectPoolDic.ContainsKey(key))
        {
            return objectPoolDic[key];
        }
        return GetInstance().ObjectPool.CreateObjectPool<T>(3, 10, args);
    }
}

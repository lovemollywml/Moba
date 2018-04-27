using UnityEngine;
using System.Collections;

public interface IPoolManager:IGameObjectPoolManager,IObjectPoolManager  {
    IObjectPoolManager ObjectPool { get; }
    IGameObjectPoolManager GameObjectPool { get; }
    GameObject GetGameObject(string poolName);
    T GetObject<T>() where T:class;
    object GetObject(string poolName);
    void ClearAll();
    void ClearAll(string poolName);
    void ClearAll<T>() where T : class;

    void RemovePool(string poolName);
    void RemovePool<T>() where T:class;

}

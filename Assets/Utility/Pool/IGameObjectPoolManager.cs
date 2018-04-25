using UnityEngine;
using System.Collections;

public interface IGameObjectPoolManager  {

    IGameObjectPool CreateGameObjectPool(string poolName, GameObject prefab, int initSize = 3, int maxSize=10);
    IGameObjectPool GetGameObjectPool(string poolName);
}
